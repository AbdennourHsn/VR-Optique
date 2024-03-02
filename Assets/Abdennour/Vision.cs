using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vision : ScriptableObject
{
    public int Id;
    public string Testname;
    public float Time;
    public bool helper;

    public AudioClip Audio;
    public bool theMainQuestion;
    [Header("Box")]
    public bool LB;

    public float xPos;

    [Header("Option")]
    public List<OptionLoin> Options=new List<OptionLoin>();

    public Dots dots;

    public abstract void SetHoles(Vision vision);
}
