using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CleanImplementation
{
    public enum Direction
    {
        LEFT,
        RIGHT
    }

    public class MenuOptions : MonoBehaviour
    {
        public MenuElement[] menuElements;
        [HideInInspector]
        public int selectedElement = 0;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {

        }

        private void Awake()
        {
            
        }

        public void SetUpOptions(List<OptionLoin> optionLoins)
        {

            if (optionLoins.Count == menuElements.Length)
            {
                for (int i = 0; i < menuElements.Length; i++)
                {
                    menuElements[i].SetUpElement(optionLoins[i]);
                    menuElements[i].UnSelect();
                }
                selectedElement = 0;
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
