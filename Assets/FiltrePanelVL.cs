using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CleanImplementation
{
    public class FiltrePanelVL : MonoBehaviour
    {
        [Header("Up")]
        public GameObject blokUp1;
        public GameObject blokUp2;

        [Header("Middle")]
        public GameObject block3_6;
        public GameObject block4_5;
        [Header("Down")]
        public GameObject blockDown1;
        public GameObject blockDown2;

        [Space(10)]
        public GameObject barrete;

        public TextMeshPro textName;

        private void HideAll()
        {
            blokUp1.SetActive(false);
            blokUp2.SetActive(false);
            block3_6.SetActive(false);
            block4_5.SetActive(false);
            blockDown1.SetActive(false);
            blockDown2.SetActive(false);
            barrete.SetActive(false);
        }

        private void ShowAll()
        {
            blokUp1.SetActive(true);
            blokUp2.SetActive(true);
            block3_6.SetActive(true);
            block4_5.SetActive(true);
            blockDown1.SetActive(true);
            blockDown2.SetActive(true);
            barrete.SetActive(false);
        }


        public void SetupFiltre(VisionLoin vision)
        {
            blokUp1.SetActive(vision.blockUp1);
            blokUp2.SetActive(vision.blockUp2);
            block3_6.SetActive(vision.block3_6);
            block4_5.SetActive(vision.block4_5);
            blockDown1.SetActive(vision.blockDown1);
            blockDown2.SetActive(vision.blockDown2);
            barrete.SetActive(vision.barrete);
        }

        public void ShowTestName(string name)
        {
            ShowAll();
            this.textName.gameObject.SetActive(true);
            this.textName.text = name;
        }

        public void HideTestName()
        {
            this.textName.gameObject.SetActive(false);
        }
    }
}