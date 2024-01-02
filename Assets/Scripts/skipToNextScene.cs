using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class skipToNextScene : MonoBehaviour
{
    private PlayerInput testActions;
    bool isloading = false;
    private void OnEnable()
    {
        testActions = GetComponent<PlayerInput>();
        testActions.onActionTriggered += HandleInputs;
    }

    private void OnDisable()
    {
        testActions.onActionTriggered -= HandleInputs;
    }

    private void HandleInputs(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log(ctx.action.name);
            if (ctx.action.name == "ToggleActionsVisibility" && !isloading)
            {
                isloading = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void Start()
    {
      
    }
}
