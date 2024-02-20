using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CleanImplementation
{
    public class FiltrePanel : MonoBehaviour
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


        private void HideAll()
        {
            blokUp1.SetActive(false);
            blokUp2.SetActive(false);
            block3_6.SetActive(false);
            block4_5.SetActive(false);
            blockDown1.SetActive(false);
            blockDown2.SetActive(false);
        }

        public void SetupFiltre(VisionLoin vision)
        {
            blokUp1.SetActive(vision.blockUp1);
            blokUp2.SetActive(vision.blockUp2);
            block3_6.SetActive(vision.block3_6);
            block4_5.SetActive(vision.block4_5);
            blockDown1.SetActive(vision.blockDown1);
            blockDown2.SetActive(vision.blockDown2);
        }
    }
}