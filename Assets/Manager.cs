using System.Collections;
using System.Collections.Generic;
using CleanImplementation;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    public GameObject All;

    public TestManager VL;
    public TestManager VI;
    //public TestManager VP;
    
    public static UnityEvent<int> OnTestDone = new UnityEvent<int>();

    private void Start()
    {
        OnTestDone.AddListener(ChangeTest);
    }

    private void OnDestroy()
    {
        OnTestDone.RemoveAllListeners();
    }

    public void RunVL()
    {
        StartCoroutine(ChangeVision(0));
    }

    public void RunVI()
    {
        StartCoroutine(ChangeVision(1));
    }

    private IEnumerator ChangeVision(int number)
    {
        VL.gameObject.SetActive(false);
        VI.gameObject.SetActive(false);
        All.SetActive(false);
        yield return new WaitForSeconds(1);
        All.SetActive(true);
        print(number);
        if (number == 1) VI.gameObject.SetActive(true);
    }

    public void ChangeTest(int num)
    {
        if (num == 0)
        {
            RunVI();
        }
        else if (num == 1)
        {
            RunVP();
        }
    }

    public void RunVP()
    {
        print("VP is not done yet");
    }
}
