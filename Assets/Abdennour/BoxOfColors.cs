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

        [Space(10)]
        [Header("Lettres")]
        public PanelOfColor panelLB1;
        public PanelOfColor panelLB2;
        public PanelOfColor panelLB3;

        [Space(10)]
        public DotsManager dotsManager;

        private void Start()
        {

        }

        public void SetBox(bool lb , float xPos , Dots dots)
        {
            panelColor1.gameObject.SetActive(!lb);
            panelColor2.gameObject.SetActive(!lb);
            panelColor3.gameObject.SetActive(!lb);

            if (!lb) MoveMiddlePanelCT(xPos);
            else MoveMiddlePanelLB(xPos);

            panelLB1.gameObject.SetActive(lb);
            panelLB2.gameObject.SetActive(lb);
            panelLB3.gameObject.SetActive(lb);

            this.dotsManager.SetupDots(dots);
        }

        public void MoveMiddlePanelCT(float xValue)
        {
            var pos = panelColor2.transform.localPosition;
            pos.x = xValue;
            panelColor2.transform.localPosition = pos;
        }

        public void MoveMiddlePanelLB(float xValue)
        {
            var pos = panelLB2.transform.localPosition;
            pos.x = xValue;
            panelLB2.transform.localPosition = pos;
        }
    }
}
