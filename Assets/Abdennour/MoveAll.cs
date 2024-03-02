using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MoveAll : MonoBehaviour
{
    public GameObject all;
    public GameObject[] far;
    public GameObject[] inter;
    public GameObject[] close;
    bool isAButtonPressed;
    public int curr = 0;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(XRInputReader.i.rightController != null &&
        //    XRInputReader.i.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 direction))
        //{
        //    if (direction.y > 0.8f)
        //    {
        //        all.transform.localPosition += Vector3.forward * Time.deltaTime*0.5f;
        //    }
        //    else if(direction.y < -0.8f)
        //    {
        //        all.transform.localPosition -= Vector3.forward * Time.deltaTime* 0.5f;
        //    }
        //}

        if (XRInputReader.i.rightController != null &&
            XRInputReader.i.rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isButtonPressed))
        {
            if (isButtonPressed && !isAButtonPressed)
            {
                timer += Time.deltaTime;
                if (timer > 1f)
                {
                    curr += 1;
                    if (curr > 2) curr = 0;
                    ChangeDistance(curr);
                    timer = 0;
                    //LoadFirstScene();
                }
            }
        }
    }

    public void ChangeDistance(int index)
    {
        HideAll();
        if (index == 0)
        {
            foreach (GameObject obj in far) obj.SetActive(true);
        }
        if (index == 1)
        {
            foreach (GameObject obj in inter) obj.SetActive(true);
        }

        if (index == 2)
        {
            foreach (GameObject obj in close) obj.SetActive(true);
        }
    }

    public void HideAll()
    {
        foreach (GameObject obj in close) obj.SetActive(false);
        foreach (GameObject obj in far) obj.SetActive(false);
        foreach (GameObject obj in inter) obj.SetActive(false);

    }
}
