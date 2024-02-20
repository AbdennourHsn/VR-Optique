using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class XRInputReader : MonoBehaviour
{
    public static XRInputReader i;
    public InputDevice rightController;
    private bool isAButtonPressed = false;

    float timer = 0;
    private void Awake()
    {
        if (i == null)
            i = this;
        else Destroy(this);

        DontDestroyOnLoad(i);
    }

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics right = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(right, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
    }

    private void Update()
    {
        if (rightController != null && rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isButtonPressed))
        {
            if (isButtonPressed && !isAButtonPressed)
            {
                timer += Time.deltaTime;
                if (timer > 5)
                {
                    //LoadFirstScene();
                }
            }
        }
    }

    public void LoadFirstScene()
    {
        timer = 0;
        SceneManager.LoadScene(0);

    }
}