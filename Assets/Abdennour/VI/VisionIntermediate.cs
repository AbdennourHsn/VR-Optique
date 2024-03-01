using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Vision", menuName = "Create Test/Vision de Intermediate", order = 2)]
public class VisionIntermediate : ScriptableObject , Vision 
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
    [Header("Up")]
    public bool blockUp1;
    public bool blockUp2;
    public bool blockUp3;
    public bool blockUp4;
    [Header("Middle")]
    public bool middle;
    [Header("Down")]
    public bool blockDowm1;
    public bool blockDowm2;
    public bool blockDowm3;
    public bool blockDowm4;

    public bool barrete;

    public float xPos;

    [Header("Option")]
    public List<OptionLoin> Options;
    
}
