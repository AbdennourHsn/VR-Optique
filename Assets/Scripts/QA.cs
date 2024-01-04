using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QA", menuName = "ScriptableObjects/Question", order = 1)]
public class QA : ScriptableObject
{
    public int id;
    public int groupID;
    public string beforAudio;
    public string name;
    public string label;
    public string questionString;
    public float time;
    public string audio;
    public static bool isRepeated = false;
    public List<string> visibales;
    public List<string> hiddens;
    public List<Option> options;
    public List<Layout> layouts;
    public string action;
    public bool toBeVerified=false;
    public bool keepSameImg = false;
}
