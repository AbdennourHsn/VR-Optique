using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FiltrePanelVI : MonoBehaviour, FiltrePanel
{
    [Header("Up")]
    [SerializeField]
    private GameObject blockUp1;
    [SerializeField]
    private GameObject blockUp2;
    [SerializeField]
    private GameObject blockUp3;
    [SerializeField]
    private GameObject blockUp4;
    [Header("Middle")]
    [SerializeField]
    private GameObject middle;
    [SerializeField]
    [Header("Down")]
    private GameObject blockDowm1;
    [SerializeField]
    private GameObject blockDowm2;
    [SerializeField]
    private GameObject blockDowm3;
    [SerializeField]
    private GameObject blockDowm4;

    [Space(10)]
    public GameObject barrete;

    public TextMeshPro textName;

    public void HideAll()
    {
        blockUp1.SetActive(false);
        blockUp2.SetActive(false);
        blockUp3.SetActive(false);
        blockUp4.SetActive(false);

        middle.SetActive(false);

        blockDowm1.SetActive(false);
        blockDowm2.SetActive(false);
        blockDowm3.SetActive(false);
        blockDowm4.SetActive(false);
        barrete.SetActive(false);
    }

    public void HideTestName()
    {
        this.textName.gameObject.SetActive(false);
    }

    public void SetupFiltre(Vision Vision)
    {
        if (Vision is VisionIntermediate vision)
        {
            blockUp1.SetActive(vision.blockUp1);
            blockUp2.SetActive(vision.blockUp2);
            blockUp3.SetActive(vision.blockUp3);
            blockUp4.SetActive(vision.blockUp4);

            middle.SetActive(vision.middle);

            blockDowm1.SetActive(vision.blockDowm1);
            blockDowm2.SetActive(vision.blockDowm2);
            blockDowm3.SetActive(vision.blockDowm3);
            blockDowm4.SetActive(vision.blockDowm4);
            barrete.SetActive(vision.barrete);
        }
    }

    public void ShowAll()
    {
        blockUp1.SetActive(true);
        blockUp2.SetActive(true);
        blockUp3.SetActive(true);
        blockUp4.SetActive(true);

        middle.SetActive(true);

        blockDowm1.SetActive(true);
        blockDowm2.SetActive(true);
        blockDowm3.SetActive(true);
        blockDowm4.SetActive(true);
        barrete.SetActive(false);
    }

    public void ShowTestName(string name)
    {
        ShowAll();
        this.textName.gameObject.SetActive(true);
        this.textName.text = name;
    }
}
