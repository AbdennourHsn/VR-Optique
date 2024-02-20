using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CleanImplementation
{
    public class MenuOptions : MonoBehaviour
    {
        public MenuElement[] menuElements;
        private int selectedElement = 0;

        private void Start()
        {
            //menuElements[selectedElement].Select();
        }

        public void SetUpOptions(List<OptionLoin> optionLoins)
        {

            if (optionLoins.Count == menuElements.Length)
            {
                for (int i = 0; i < menuElements.Length; i++)
                {
                    menuElements[i].SetUpElement(optionLoins[i].img, optionLoins[i].imgSelected);
                }
                menuElements[selectedElement].Select();
            }
        }

        private void UnSelectAll()
        {
            foreach (MenuElement element in menuElements) element.UnSelect();
        }

        public void NextElement()
        {
            UnSelectAll();
            selectedElement += 1;
            if (selectedElement >= menuElements.Length) selectedElement = 0;
            menuElements[selectedElement].Select();
        }

        public void PreviousElement()
        {
            UnSelectAll();
            selectedElement -= 1;
            if (selectedElement < 0) selectedElement = menuElements.Length - 1;
            menuElements[selectedElement].Select();
        }
    }
}
