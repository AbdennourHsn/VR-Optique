using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class loadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;

    public int delay;
    int time=0;
    //public List<VideoClip> clips;



    public AudioSource buttonPressSound;
    public Animator animator;

    void HandleInputs(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log(ctx.action.name);
            if (ctx.action.name == "ToggleActionsVisibility")
            {
                print("here this stupid is");
                loadNextLevel();
            }
        }
    }


    void Start()
    {
        LoadNextScene();
    }
    async void LoadNextScene()
    {
        if (delay==15)
        {
            delay = 23;
        }
        await testRunner.WaitForSeconds(delay);
        //fadeToLevel();
        //whenFadeComplete(sceneId);
        loadNextLevel();
    }
    public async static void _loadScene(float delay,string sceneName,Animator animator)
    {
        
        animator.SetTrigger("fadeOut");
        await testRunner.WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    private void loadNextLevel()
    {
        if (this != null)
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1.5f);
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

        
    }
     void Update()
    {
        time++;
    }
}
