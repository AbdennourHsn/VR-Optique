using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Vision", menuName = "Create Test/Vision de Loin", order = 1)]
public class VisionLoin : Vision
{
    [Header("Plaque Filtre")]
    public bool blockUp1;
    public bool blockUp2;

    public bool block3_6;
    public bool block4_5;

    public bool blockDown1;
    public bool blockDown2;

    public bool barrete;

    public VisionLoin()
    {

    }

    public VisionLoin(Vision vision)
    {
        this.Id = vision.Id;
        this.testId = vision.testId;
        this.Testname = vision.Testname;
        this.Time = vision.Time;
        this.helper = vision.helper;
        this.Audio = vision.Audio;
        this.theMainQuestion = vision.theMainQuestion;
        this.LB = vision.LB;
        this.xPos = vision.xPos;
        this.dots = vision.dots;
        foreach(OptionLoin option in vision.Options)
        {
            this.Options.Add(option);
        }
    }

    public override void SetHoles(Vision vision)
    {
        if(vision is VisionLoin visionLoin)
        {
            blockUp1 = visionLoin.blockUp1;
            blockUp2 = visionLoin.blockUp2;
            block4_5 = visionLoin.block4_5;
            block3_6 = visionLoin.block3_6;
            blockDown1 = visionLoin.blockDown1;
            blockDown2 = visionLoin.blockDown2;
            barrete = visionLoin.barrete;
        }
    }
}

[System.Serializable]
public struct OptionLoin
{
    public string Label;
    public Sprite img;
    public Sprite imgSelected;
    public Vision next;
    public bool isLast;
    public Results ResultsCode;
}


