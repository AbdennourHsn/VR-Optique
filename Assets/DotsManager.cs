using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Dots
{
    public bool dotUp1;
    public bool dotUp2;
    public bool dotMiddle1;
    public bool dotMiddle2;
    public bool dotDown1;
    public bool dotDown2;
}
public class DotsManager : MonoBehaviour
{
    [Header("Up")]
    [SerializeField]
    private GameObject dotUp1;
    [SerializeField]
    private GameObject dotUp2;
    [Header("Middle")]
    [SerializeField]
    private GameObject dotMiddle1;
    [SerializeField]
    private GameObject dotMiddle2;
    [Header("Down")]
    [SerializeField]
    private GameObject dotDown1;
    [SerializeField]
    private GameObject dotDown2;


    public void SetupDots(Dots dots)
    {
        dotUp1.SetActive(dots.dotUp1);
        dotUp2.SetActive(dots.dotUp2);
        dotMiddle1.SetActive(dots.dotMiddle1);
        dotMiddle2.SetActive(dots.dotMiddle2);
        dotDown1.SetActive(dots.dotDown1);
        dotDown2.SetActive(dots.dotDown2);
    }

    public void HideAll()
    {
        dotUp1.SetActive(false);
        dotUp2.SetActive(false);
        dotMiddle1.SetActive(false);
        dotMiddle2.SetActive(false);
        dotDown1.SetActive(false);
        dotDown2.SetActive(false);
    }
}
