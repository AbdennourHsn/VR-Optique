
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class TutorialInput : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer videoPlayer;
    public List<long> ts;


    private bool canResume;
    private int currentPause = 0;

    //public List<VideoClip> clips;
    private PlayerInput testActions;

    public AudioSource buttonPressSound;
    public Animator animator;
    private void Awake()
    {
        testActions = transform.GetComponent<PlayerInput>();
        testActions.onActionTriggered += HandleInputs;
    }

    void HandleInputs(InputAction.CallbackContext ctx)
    {
        
        if (ctx.performed)
        {
            Debug.Log(ctx.action.name);
            currentPause = ResumeTutorial(videoPlayer,  currentPause, ctx.action.name);
            if (currentPause==ts.Count || ctx.action.name==actions[actions.Count-1])
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    List<string> actions = new List<string>();
    private int ResumeTutorial(VideoPlayer videoPlayer,  int tsIndex, string actionName)
    {
        Debug.Log("Current Frame = " + videoPlayer.frame + " | Total Frames =" + videoPlayer.frameCount+ " | currentPaus = "+ tsIndex);
        if (tsIndex<ts.Count)
        {
            if (videoPlayer.isPaused && ts[tsIndex] == videoPlayer.frame && actions[tsIndex]==actionName )
            {
                videoPlayer.Play();
                Debug.Log("is Playing"+ videoPlayer.isPlaying+" "+ videoPlayer.frame);
                return  tsIndex+1;
            }
        }

        return tsIndex;
    }
 
    void Start()
    {
        //videoPlayer.frame = 1100;
        actions.Add("pause");
        actions.Add("Navigation");
        actions.Add("saveAnswer");
        actions.Add("backToPrevious");
        actions.Add("ToggleSummery");
        actions.Add("ToggleActionsVisibility");
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(videoPlayer.frame);
        if (videoPlayer.frame >= ts[currentPause] && videoPlayer.isPlaying)
        {
            //Debug.Log(videoPlayer.frame);
            videoPlayer.Pause();
            canResume = false;
        }
        else if (videoPlayer.isPaused && canResume)
        {
            videoPlayer.Play();
        }
    }
}
