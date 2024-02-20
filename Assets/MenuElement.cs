using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CleanImplementation
{
    public class MenuElement : MonoBehaviour
    {
        public Image image;
        [Header("Images")]
        public Sprite img;
        public Sprite imgSelected;

        [Header("Triggers")]
        bool isSelected;

        public UnityEvent Next;

        public void SetUpElement(Sprite img, Sprite imgSelected)
        {
            this.img = img;
            this.imgSelected = imgSelected;
            image.sprite = this.img;
        }

        public void SetupEvent(UnityAction method)
        {
            Next.AddListener(method);
        }

        public void Select()
        {
            isSelected = true;
            this.transform.localScale = Vector3.one * 1.5f;
            image.sprite = imgSelected;
        }

        public void UnSelect()
        {
            isSelected = false;
            this.transform.localScale = Vector3.one * 1f;
            image.sprite = img;
        }
        public void Trigger()
        {
            Next?.Invoke();
        }
    }
}