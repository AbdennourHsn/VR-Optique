using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;

[System.Serializable]
public class ObjectState
{
    public GameObject gameObject;
    public bool isActive;
    public Vector3 position;
    // Add any other relevant properties here
}

public class TestRunner : MonoBehaviour
{
    public bool tobeverifie = true;
    public int groupId=0;
    public QA[] qas;
    public QA verificationQuestion;
    public QA verificationQuestionNo;
    public Questions AllQuestions;
    //ui elements
  
    public TextMeshProUGUI questionUI;
    public TextMeshProUGUI visibleOptionsTxt;

    public Image loading;

    private List<ObjectState> objectStates = new List<ObjectState>();
    private bool acceptInputs = true;
    private bool optionsVisible = true;
    //sound
    public AudioSource uiSwitch;
    public AudioSource uiAnswer;
    public AudioSource narrator;

    private PlayerInput testActions;
    
    public int currentPos = 0;
    public List<Image> optinsGroup = new List<Image>();
    public static List<List<object>> Routes;

    public List<Image> _2s;
    public List<Image> _3s;
    public List<Image> _4s;
    public List<Image> _5s;
    public List<Image> _6s;
    Thread tread;
    List<int> previous =new List<int>();
    public static Dictionary<string, Vector3> sceneInitialState = new Dictionary<string, Vector3>();
    private bool toggleSummeryView = true; //all test summery screen toggle
    public List<GameObject> summeyUI = new List<GameObject>();
    public GameObject pauseMenu;
    /// <summary>
    /// setting input system along with images options
    /// </summary>
    private void Awake()
    {
        testActions = transform.GetComponent<PlayerInput>();
        testActions.onActionTriggered += HandleInputs;

    }

    void Start()
    {
        //save initialState of the scene in all objects
        // hide options gameObject
        optionsToogle(false);   //hide all option groups 2,3,4,5 
        questionUI.enabled = false; //hide question test
        ConfigsManager.LoadQuestionFromResources(); //load json questions
        AllQuestions = new Questions(qas);
        //AllQuestions = ConfigsManager.Q; // set the load questions from questions.json
        loadQuestion(currentPos);
        if (output != null)
            output.text = ConfigsManager.screens[0];

    }
    /// <summary>
    /// joystick direction
    /// </summary>
    private enum Direction
    {
        LEFT,
        RIGHT
    }


    void HandleInputs(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed && ctx.action.name == "pause")
        {
            // check if game pause
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (ctx.performed && acceptInputs )
        {
            if (ctx.action.name == "backToPrevious")
            {
                if (optionsVisible && !isPaused)
                {
                    if (previous.Count != 0)
                    {
                        if (previous.Count>0)
                        {
                            currentPos = previous[previous.Count-1];
                            previous.RemoveAt(previous.Count - 1);
                            loadQuestion(currentPos);
                           // previous.Clear();
                        }

                    }
                }

            }
            else if (ctx.action.name == "ToggleActionsVisibility")
            {
                if (!isPaused)
                {
                    // toggle options visibility
                    optionsVisible = !optionsVisible;
                    hideAllOnScreenImages();
                    foreach (Image item in optinsGroup)
                    {
                        item.enabled = optionsVisible;
                    }
                    visibleOptionsTxt.enabled = optionsVisible?false:true;
                }

            }
            else if (ctx.action.name == "saveAnswer")
            {
                // Answer the current Question and move to the Next
                if (optionsVisible && !isPaused)
                {
                    Next();
                }
            }
            else if (ctx.action.name == "Navigation")
            {
                Direction d=Direction.RIGHT;
                var input = ctx.ReadValue<Vector2>();
                if (optionsVisible && !isPaused)
                {
                    d = input.x > 0? Direction.RIGHT: Direction.LEFT;


                    if (Math.Abs(input.x) > .5) // horizontal axis is pressed
                    {
                        swithCurrentAnswer(d);
                    }
                }
                if (toggleSummeryView)
                {
                    //Debug.Log("Switch");
                    var output = summeyUI[0].GetComponentsInChildren<TextMeshProUGUI>().First();

                    int currntScreen = ConfigsManager.screens.IndexOf(ConfigsManager.screens.Find(I => I == output.text).ToString());

                    int NewScreen = d == Direction.LEFT ? currntScreen - 1 : currntScreen + 1;

                    if (NewScreen < 0)
                    {
                        NewScreen = ConfigsManager.screens.Count - 1;
                    }
                    else if (NewScreen >= ConfigsManager.screens.Count)
                    {
                        NewScreen = 0;
                    }
                    output.text = ConfigsManager.screens[NewScreen];


                }

            }
            else if (ctx.action.name== "ToggleSummery")
            {
                if ( !isPaused)
                {
                    //toogle the following
                    //image
                    toggleSummeryView = !toggleSummeryView;
                    optionsVisible = !toggleSummeryView;
                    foreach (GameObject item in summeyUI)
                    {
                        item.SetActive(toggleSummeryView);
                    }
                    questionUI.enabled = !toggleSummeryView;
                }

            }
            else if (ctx.action.name == "pause")
            {
               
            }
            else if (ctx.action.name == "navkey")
            {
                if (optionsVisible && !isPaused)
                {
                Direction d = Direction.RIGHT;
                swithCurrentAnswer(d);

                }
            }
        }
    }

    private void ResumeGame()
    {
       pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        if (!narrator.isPlaying && acceptInputs)
        {
            narrator.Play();
        }
        isPaused = false;
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        if (narrator.isPlaying)
        {
            narrator.Pause();
        }
        isPaused = true;
    }
    bool isPaused=false;
    public TextMeshProUGUI output;

    private void Update()
    {
       // Debug.Log(uiText.transform.position);
    }

    void optionsToogle(bool toogle)
    {
        foreach (Image img in _2s) img.enabled = toogle;
        foreach (Image img in _3s) img.enabled = toogle;
        foreach (Image img in _4s) img.enabled = toogle;

    }
    
    void UpdateUIImageWithQuestionOptions(List<Image> UiImages,List<Option> options)
    {
        foreach (Option op in options)
        {
            int index = options.IndexOf(op);
            //Debug.Log(op.label + " -  " + index +" -- "+ UiImages[index].sprite.name +"  -- "+ options[index].iSselected);
            if (op.iSselected)
            {
                // selected
                UiImages[index].sprite = Resources.Load<Sprite>("UI-images/"+op.image_selected);
                UiImages[index].transform.localScale = new Vector3(1.5f,1.5f,1.5f);

            }
            else if(!op.iSselected)
            {
                // not selected
                UiImages[index].sprite = Resources.Load<Sprite>("UI-images/"+op.image_name);
                UiImages[index].transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    void clearSetup()
    {

        // set all game objects as active
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (obj.tag!="hidden")
            {
                obj.SetActive(true);

                // rest any movable object to it's original position (if any)
                if (ConfigsManager.offsetedObjectXValueList.ContainsKey(obj.name))
                {
                
                    float initalX = ConfigsManager.offsetedObjectXValueList[obj.name];
                    if (obj.name=="2R"|| obj.name == "2G"|| obj.name == "2Y"|| obj.name == "2B")
                    {
                        initalX = 0f;
                    }
                    obj.transform.position = new Vector3(initalX, obj.transform.position.y, obj.transform.position.z);
               
                }

                
            }

        }

        //hide all onscreen options
        hideAllOnScreenImages();
        pauseMenu.SetActive(false);

    }

    public void hideAllOnScreenImages()
    {
        foreach (Image ob in _2s) ob.enabled = false;
        foreach (Image ob in _3s) ob.enabled = false;
        foreach (Image ob in _4s) ob.enabled = false;
        foreach (Image ob in _5s) ob.enabled = false;
        foreach (Image ob in _6s) ob.enabled = false;
        summeyUI[0].SetActive(false);
    }

    public static async Task WaitForSeconds(float seconds)
    {
        await Task.Delay((int)(seconds * 1000));
    }

   

    private void finishTest()
    {
        Debug.Log("All test finished");
        // when the test finishs we need to save answers somwhere 
        questionUI.enabled = true;
        questionUI.text = "!!! tout les tests est fait, Cliquez sur \n[stick] pour afficher la synth�se !!!";

        // summary of all stats



    }

    List<int> SummarizeVisionType( string label,int start, int end)
    {
        int correctAmmount = 0;
        int simpleDefectAmmount = 0;
        int ImoportantDefectAmmount = 0;
        foreach (string testName in ConfigsManager.answers.Keys)
        {
            int testNumber = int.Parse(testName.Split('N')[1]);
            if (testNumber >= start && testNumber <= end)
            {
                if (ConfigsManager.answers[testName] == "COR" || ConfigsManager.answers[testName] == "INH" || ConfigsManager.answers[testName] == "INH+")
                {
                    correctAmmount++;
                }
                else if (ConfigsManager.answers[testName] == "NS" || ConfigsManager.answers[testName] == "HO" || ConfigsManager.answers[testName] == "HR" || ConfigsManager.answers[testName].StartsWith("NT"))
                {
                    simpleDefectAmmount++;
                }
                else
                {
                    ImoportantDefectAmmount++;
                }
            }
        }
        List<int> stats = new List<int>();

        stats.Add(correctAmmount);
        stats.Add(simpleDefectAmmount);
        stats.Add(ImoportantDefectAmmount);
        return stats;

    }

    /// <summary>
    /// Move to the next option
    /// </summary>
    void swithCurrentAnswer(Direction d)
    {
        if (optionsVisible)
        {
            if (optinsGroup.Count > 0)
            {
                // get reference to current question 
                Question current = AllQuestions.questions.Where(q=>q.id== currentPos).First();

                // get selected option 
                Option o = current.selectedOption();
                // move to next option (if you already in the last one move to first one)

                int currentOption = current.options.IndexOf(o);
                
                //get current answer
                //Debug.Log("B "+(currentOption+1) + " OF " + current.options.Count);
                currentOption = d == Direction.RIGHT ? currentOption + 1 : currentOption - 1;
                if (currentOption == current.options.Count )
                {
                    currentOption = 0;
                }
                if( currentOption < 0) { currentOption = current.options.Count-1; }
                //Debug.Log("A " + (currentOption+1) + " OF " + current.options.Count);


                // select the current option
                current.selectOption(currentOption);
                UpdateUIImageWithQuestionOptions(optinsGroup, current.options);
                uiSwitch.Play();//play the sound of switching answers
            }
        }
        
        //match optinsGroup with current questio selection

    }
    public float titleVisibilityInterval;

    private void CaptureObjectStates()
    {
        objectStates.Clear();
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            // Exclude the Camera and Canvas from capturing their state
            if (obj.GetComponent<Camera>() != null || obj.GetComponent<Canvas>() != null || obj.name== "--------UI"||obj.name == "------LOGIC -----")
                continue;

            ObjectState state = new ObjectState();
            state.gameObject = obj;
            state.isActive = obj.activeSelf;
            state.position = obj.transform.position;

            // Add any other relevant properties to 'state'

            objectStates.Add(state);
        }
    }
    TextMeshProUGUI uiText;
    public List<GameObject> covers;
    private void HideAllObjects(Question q)
    {
        foreach (ObjectState state in objectStates)
        {
            // Exclude hiding the Camera and Canvas
            if (state.gameObject.GetComponent<Camera>() != null || state.gameObject.GetComponent<Canvas>() != null || state.gameObject.name == "--------UI"||state.gameObject.name== "------LOGIC -----" || state.gameObject.name== "video background") 
                continue;

            state.gameObject.SetActive(false);
           
        }

        // Add UI Text object as a child to the Canvas
        GameObject canvas = GameObject.Find("Canvas"); // Assuming the Canvas GameObject is named "Canvas"


        if (canvas != null)
        {
            GameObject uiTextObject = new GameObject("UITextObject");
            uiTextObject.transform.SetParent(canvas.transform, false);

            uiText = uiTextObject.AddComponent<TextMeshProUGUI>();
            uiText.text = q.label;
            uiText.fontSize = 1.6f;
            uiText.transform.position =new  Vector3(0.0f,.85f,0.56f);
            uiText.alignment = TextAlignmentOptions.Center;
            // show cover pannel based on curret question

            if (q.id >= 0 && q.id <=58)
            {
                //show v loin cover
                covers[0].SetActive(true);
                covers[1].SetActive(false);
                covers[2].SetActive(false);
                uiText.transform.localPosition= new Vector3(uiText.transform.localPosition.x, uiText.transform.localPosition.y, covers[0].transform.localPosition.z - .01f)  ;
            }
            else if (q.id >= 59 && q.id <= 104)
            {
                //show v intermedier cover
                covers[1].SetActive(true);
                covers[0].SetActive(false);
                covers[2].SetActive(false);
                uiText.transform.localPosition = new Vector3(2.6f,-14f,-16.7f);
                uiText.fontSize = 1f;
            }
            else
            {
                // show v pres cover
                covers[2].SetActive(true);
                covers[0].SetActive(false);
                covers[1].SetActive(false);
                uiText.transform.localPosition = new Vector3(1.98f, -15.26f, -30.9f);
                uiText.fontSize = 1f;
            }
        }
        
    }

    private void RestoreObjectStates()
    {
        uiText.text = "";
        foreach (ObjectState state in objectStates)
        {
            // Exclude restoring the Camera and Canvas
            if (state.gameObject.GetComponent<Camera>() != null || state.gameObject.GetComponent<Canvas>() != null)
                continue;

            state.gameObject.SetActive(state.isActive);
            state.gameObject.transform.position = state.position;
            // Restore any other relevant properties of the object if needed

        }
    }
    async void loadQuestion(int qIndex )
    {
        pauseMenu.SetActive(false);
        if (qIndex!=-1)
        {
            summeyUI[0].active = false;
            //TODO:: remove question id for visibleOotionsTxt
            visibleOptionsTxt.text = "les options sont masqu�es,\nappuyez sur le boutton[GRAB]\npour les afficher\n Q-id = " + qIndex;
            visibleOptionsTxt.enabled = false;


            Question q = AllQuestions.questions.Where(qq=>qq.id==qIndex).First();

            if (questionUI != null) questionUI.enabled = false;
            if (loading != null)
                loading.enabled = true;
            acceptInputs = false;
            //play question before audio if exists
            if (q.beforAudio != "" && !Question.isRepeated)
            {
                LoadAudio(q.beforAudio);
            }


            if(!q.keepSameImg)clearSetup();

            q.init();

            if(isTestJustStarted(qIndex))
            {
                CaptureObjectStates();

                // Step 2: Hide all objects in the scene
                HideAllObjects(q);

                // Step 3: Show the testTitle object for a specific duration (adjust 'titleVisibilityInterval' as needed)
                await WaitForSeconds(titleVisibilityInterval); // Wait for the specified duration

                // Step 4: Reapply the captured object states
                RestoreObjectStates();
            }


            float oldtitleVisibilityInterval = titleVisibilityInterval;
            switch (q.audio)
            {
                case "image changed":
                    titleVisibilityInterval = 2;
                    break;
                case "image stable":
                    titleVisibilityInterval = 3;
                    break;
            }

            await WaitForSeconds(q.time);
            //Hide waiting icon
            if (loading != null) // check if loading image is active
                loading.enabled = false;


            //show question text 
            if (questionUI != null)
            {
                questionUI.enabled = true;
                questionUI.text = "";
                //questionUI.text = q.questionString;
                questionUI.enabled = true;
                // show title only in firs question of the test
                
                //if (configsmanager.beginTestIds.Contains(qIndex))
                //{
                //    if (qIndex > 0)
                //    { Debug.Log("C: " + q.name + " P: " + Q.questions[qIndex - 1].name); }
                    
                //}

            }
            

            //play question audio
            LoadAudio(q.audio);
            optionsVisible = false;
            await WaitForSeconds(titleVisibilityInterval);
            if (questionUI!=null)
            {
            questionUI.enabled = true;

            }
            acceptInputs = true;
            titleVisibilityInterval = oldtitleVisibilityInterval;
            questionUI.enabled = false;
            optionsVisible = true;
            // show questions options as images with the default as selected
            LoadQuestionOptions(q);

        }
        else
        {
            finishTest();
            
        }
    }

    private bool isTestJustStarted(int qIndex)
    {
        return ConfigsManager.beginTestIds.Contains(qIndex);
    }

    private void LoadAudio(string AudioName)
    {
        AudioClip audioClip = Resources.Load<AudioClip>("audio script/" + AudioName);
        narrator.Stop();
        if (audioClip != null)
        {
            narrator.clip = audioClip;
            narrator.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogError(audioClip+" Audio clip not found!");
        }

    }

    public void LoadQuestionOptions(Question q)
    {
        switch (q.options.Count)
        {
            case 2:
                // show 2 options group
               
                    foreach (Image item in _2s)
                    {
                        if (item!=null)
                          item.enabled = true;
                    }
                    optinsGroup = _2s;
                break;
            case 3:
                foreach (Image item in _3s)
                {
                    item.enabled = true;
                }
                optinsGroup = _3s;
                break;
            case 4:
                foreach (Image item in _4s)
                {
                    item.enabled = true;
                }
                optinsGroup = _4s;
                break;
            case 5:
                foreach (Image item in _5s)
                {
                    item.enabled = true;
                }
                optinsGroup = _5s;
                break;
            case 6:
                foreach (Image item in _6s)
                {
                    item.enabled = true;
                }
                optinsGroup = _6s;
                break;
        }

        // show correct sprites on current options
        UpdateUIImageWithQuestionOptions(optinsGroup, q.options);

    }

    public Question VerifieQuestion( string aname , Option  oui, Question no)
    {
        verificationQuestion.name = aname;
        verificationQuestion.groupID = no.GroupId;
        Question question = new Question(verificationQuestion);
        question.options[0].nextQ = oui.nextQ;
        question.options[0].resultCode = oui.resultCode;
        verificationQuestionNo.name = aname;
        int x = AllQuestions.questions.Where(q => q.GroupId == no.GroupId + 1).First().id;

        Question qNo= AllQuestions.questions.Where(q => q.GroupId == no.GroupId && q.questionString== "Combien de cercles voyez-vous ?").First();

        List<Option> optionsToNo=new List<Option>();
        optionsToNo.AddRange(qNo.options);
        optionsToNo.Add(new Option
        {
            label = "image non-stable",
            image_name = "NonStable",
            image_selected = "NonStable-S",
            nextQ = x,
            resultCode = "NS",
        });
        verificationQuestionNo.options = optionsToNo;
        verificationQuestionNo.groupID = no.GroupId;
        Question b = AllQuestions.questions.FirstOrDefault(q => q.id == 1000);
        b.UpdateValues(verificationQuestion);
        Question a = AllQuestions.questions.FirstOrDefault(q => q.id == 2000);
        a.UpdateValues(verificationQuestionNo);
        question.options[1].nextQ = 2000;
        return question;
    }


    public void Next()
    {
        previous.Add(currentPos);// add to buffer history
        Question current = AllQuestions.questions.Where(q=>q.id == currentPos).First();

        if(current.GroupId !=groupId)
        {
            print("Next group");
            groupId = current.GroupId;
            tobeverifie = true;
        }
        //Debug.Log(current.selectedOption().label+" "+ current.selectedOption().resultCode + " " + current.selectedOption().nextQ);

        uiAnswer.Play();//button press sound
        optionsToogle(false);
        questionUI.enabled = false;
        int nextQ = current.selectedOption().nextQ;
        hideAllOnScreenImages();
        if (nextQ != -1)
        {

            if (current.selectedOption().resultCode!="")
            {
                if (tobeverifie)
                {
                    tobeverifie = false;
                    Question verifir = VerifieQuestion(current.name, current.selectedOption(), current);
                    nextQ = verifir.id;
                    currentPos = nextQ;
                    hideAllOnScreenImages();
                    loadQuestion(currentPos);
                    Debug.Log(current.name + "  => " + current.selectedOption().resultCode + " id-> " + current.id);
                    TestSaver.instance.SendDataToServer(current.name, current.selectedOption().resultCode, false);
                    return;
                }
                    string testName = current.name;
                    // unsaved results to to be saved for generating the summery
                    int currntScreen = ConfigsManager.screens.IndexOf(ConfigsManager.screens.Find(I => I == output.text).ToString());
                    //ConfigsManager.saveAnswer(testName, current.selectedOption().resultCode);
                    Debug.Log(current.name + "  => " + current.selectedOption().resultCode + " id-> " + current.id);
                    TestSaver.instance.SendDataToServer(current.name, current.selectedOption().resultCode , true);
                    output.text = ConfigsManager.screens[currntScreen];
            }
            if (nextQ < currentPos && !Question.isRepeated)
            {
                Question.isRepeated=true;
            }

            currentPos = nextQ;
            hideAllOnScreenImages();
            loadQuestion(currentPos);
        }
        else finishTest();

    }
}
