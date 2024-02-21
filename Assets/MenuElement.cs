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
        public TestManager testManager;
        [Header("Images")]
        public Sprite img;
        public Sprite imgSelected;

        [Header("Triggers")]
        public bool isLast;
        public Results resultat;

        public VisionLoin nextQestion;

        bool isSelected;

        public UnityEvent Next;

        public void SetUpElement(OptionLoin option)
        {
            this.img = option.img;
            this.imgSelected = option.imgSelected;
            image.sprite = this.img;
            this.nextQestion = option.next;
            this.isLast = option.isLast;
            this.resultat = option.ResultsCode;
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
            if (isLast)
            {
                testManager.VerifieQuestion(resultat);
            }
            else
            {
                testManager.ChangeQuestion(nextQestion);
            }
        }
    }
}