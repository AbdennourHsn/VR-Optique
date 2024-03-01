using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Vision", menuName = "Create Test/Vision de Loin", order = 1)]
public class VisionLoin : ScriptableObject , Vision
{
    public int Id;
    public string Testname;
    public float Time;
    public bool helper;

    public AudioClip Audio;
    public bool theMainQuestion;
    [Header("Box")]
    public bool LB;

    [Header("Plaque Filtre")]
    public bool blockUp1;
    public bool blockUp2;

    public bool block3_6;
    public bool block4_5;

    public bool blockDown1;
    public bool blockDown2;

    public bool barrete;

    public float xPos;

    [Header("Option")]
    public List<OptionLoin> Options;
    


}

[System.Serializable]
public struct OptionLoin
{
    public string Label;
    public Sprite img;
    public Sprite imgSelected;
    public VisionLoin next;
    public bool isLast;
    public Results ResultsCode;
}


