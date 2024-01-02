using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class toggleImgs : MonoBehaviour
{
    public  List<Image> imgs;
    private PlayerInput testActions;
    public TestRunner ts;
    // Start is called before the first frame update
    private void Awake()
    {
        testActions = transform.GetComponent<PlayerInput>();
        testActions.onActionTriggered += HandleInputs;
    }
    void HandleInputs(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

            if (ctx.action.name == "SelectAnswer")
            {
                swithCurrentImage(ctx.action.name);
            }
            else  // Answer the current Question
            {
                swithCurrentImage(ctx.action.name);
            }
        }
    }
    void Start()
    {
        foreach (Image img in imgs)
        {
           img.enabled = false;
    
        }
    }
    int picsIndex = -1;
    private void swithCurrentImage(string actionName)
    {
        if (ts.currentPos == 5 )
        {
            if (picsIndex == -1)
            {
                //stop current audio
                ts.narrator.Stop();
                // play images audio
                AudioClip audioClip = Resources.Load<AudioClip>("audio script/pictures audio");
                if (audioClip != null)
                {
                    ts.narrator.clip = audioClip;
                    ts.narrator.PlayOneShot(audioClip);
                }
                else
                {
                    Debug.LogError("Audio clip not found!");
                }
                //show images
                foreach (Image image in imgs)
                {
                    image.enabled = true;
                }
                //hide question
                ts.questionUI.enabled = false;
                // hide answers
                foreach(var option in  ts.optinsGroup)
                {
                    option.enabled = false;
                }
                picsIndex = 0;
                inspectImage(picsIndex);
            }
            else
            {
                picsIndex++;
                if (picsIndex==imgs.Count)
                {
                    picsIndex = 0;
                }
                // inspect Next Image image
                inspectImage(picsIndex);
            }

        }
        else if(actionName != "SelectAnswer" || ts.currentPos!=5)
        {
            // hide all images 
            foreach (Image image in imgs)
            {
                image.enabled = false;
            }
            // show possible answers
            foreach (var option in ts.optinsGroup)
            {
                option.enabled = true;
            }
            // how questions 
            ts.questionUI.enabled = true;
        }
    }

    void inspectImage(int picIndx)
    {
        // scale all images in 0.8 x and y
        foreach (Image img in imgs)
        {
            img.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        }
        imgs[picIndx].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
        // scale current image by 1.2 x and y
    }
}
