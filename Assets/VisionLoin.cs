using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Vision", menuName = "ScriptableObjects/Vision de Loin", order = 1)]
public class VisionLoin : ScriptableObject
{
    public int Id;
    public string Testname;
    public float Time;
    public AudioClip Audio;
    [Header("Box")]
    public bool LB;

    [Header("Plaque Filtre")]
    public bool blockUp1;
    public bool blockUp2;

    public bool block3_6;
    public bool block4_5;

    public bool blockDown1;
    public bool blockDown2;

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

