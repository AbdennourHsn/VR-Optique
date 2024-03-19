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
        public string message;

        [Header("Triggers")]
        public bool isLast;
        public Results resultat;

        public Vision nextQestion;

        bool isSelected;

        public UnityEvent Next;

        public float scaled=1;

        private void Start()
        {
            
        }

        public void SetUpElement(OptionLoin option)
        {
            this.img = option.img;
            this.imgSelected = option.imgSelected;
            image.sprite = this.img;
            this.message = option.message;
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
            this.transform.localScale = Vector3.one * (scaled*1.5f);
            image.sprite = imgSelected;
            UiManager.OnSetMessage?.Invoke(this.message);
        }

        public void UnSelect()
        {
            isSelected = false;
            this.transform.localScale = Vector3.one * scaled;
            image.sprite = img;
        }
        public void Trigger()
        {
            if (isLast)
            {
                TestManager.instance.VerifieQuestion(resultat);
            }
            else
            {
                TestManager.instance.ChangeQuestion(nextQestion);
            }
        }
    }
}