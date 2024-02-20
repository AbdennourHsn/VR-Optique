using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CleanImplementation
{
    public class BoxOfColors : MonoBehaviour
    {
        [Header("Colors")]
        public PanelOfColor panelColor1;
        public PanelOfColor panelColor2;
        public PanelOfColor panelColor3;
        [Header("Lettres")]
        public PanelOfColor panelLB1;
        public PanelOfColor panelLB2;
        public PanelOfColor panelLB3;

        public void SetBox(bool lb)
        {
            panelColor1.gameObject.SetActive(!lb);
            panelColor2.gameObject.SetActive(!lb);
            panelColor3.gameObject.SetActive(!lb);

            panelLB1.gameObject.SetActive(lb);
            panelLB2.gameObject.SetActive(lb);
            panelLB3.gameObject.SetActive(lb);


        }
    }
}
