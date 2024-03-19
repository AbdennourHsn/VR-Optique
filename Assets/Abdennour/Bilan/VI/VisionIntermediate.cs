using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Vision", menuName = "Create Test/Vision de Intermediate", order = 2)]
public class VisionIntermediate : Vision 
{
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

    public VisionIntermediate()
    {

    }

    public VisionIntermediate(Vision vision)
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
        foreach (OptionLoin option in vision.Options)
        {
            this.Options.Add(option);
        }
    }

    public override void SetHoles(Vision vision)
    {
        if(vision is VisionIntermediate visionIntermediate)
        {
            blockUp1 = visionIntermediate.blockUp1;
            blockUp2 = visionIntermediate.blockUp2;
            blockUp3 = visionIntermediate.blockUp3;
            blockUp4 = visionIntermediate.blockUp4;

            middle = visionIntermediate.middle;

            blockDowm1 = visionIntermediate.blockDowm1;
            blockDowm2 = visionIntermediate.blockDowm2;
            blockDowm3 = visionIntermediate.blockDowm3;
            blockDowm4 = visionIntermediate.blockDowm4;

            barrete = visionIntermediate.barrete;
        }
    }
}
