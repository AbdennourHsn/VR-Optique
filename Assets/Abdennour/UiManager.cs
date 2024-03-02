using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CleanImplementation
{
    public class UiManager : MonoBehaviour
    {
        [Header("Menu Options")]
        [SerializeField]
        private MenuOptions twoOptions;
        [SerializeField]
        private MenuOptions treeOptions ;
        [SerializeField]
        private MenuOptions foorOptions;
        [SerializeField]
        private MenuOptions fiveOptions;
        [SerializeField]
        private MenuOptions sixOptions;


        private MenuOptions currOptions;

        [Space(10)]
        [Header("Navigations settings")]

        [SerializeField]
        private AudioSource UiAudioSource;

        [SerializeField]
        AudioClip slide, ok;

        public PlayerInput inputHandler;

        private void Awake()
        {
            if (inputHandler != null)
            {
                InputAction Navigation = inputHandler.actions["Navigation"];
                InputAction select = inputHandler.actions["saveAnswer"];
                if (Navigation != null)
                {
                    Navigation.performed += ChangeOption;
                }
                else
                {
                    Debug.LogError("Navigation action not found!");
                }
                if (select != null)
                {
                    select.performed += Choose;
                }
                else
                {
                    Debug.LogError("Select action not found!");
                }
            }
            else
            {
                Debug.LogError("PlayerInput component not found!");
            }
        }

        private void ChangeOption(InputAction.CallbackContext ctx)
        {
            Direction dir = Direction.RIGHT;
            var input = ctx.ReadValue<Vector2>();
            dir = input.x > 0 ? Direction.RIGHT : Direction.LEFT;
            if (Math.Abs(input.x) > .5)
            {
                if (dir == Direction.LEFT)
                {
                    PlaySound(slide);
                    currOptions.PreviousElement();
                }
                else if (dir == Direction.RIGHT)
                {
                    PlaySound(slide);
                    currOptions.NextElement();
                }
            }
        }

        private void Choose(InputAction.CallbackContext ctx)
        {
            PlaySound(ok);
            currOptions.menuElements[currOptions.selectedElement].Trigger();
        }

        public void HideAll()
        {
            twoOptions.gameObject.SetActive(false);
            treeOptions.gameObject.SetActive(false);
            foorOptions.gameObject.SetActive(false);
            fiveOptions.gameObject.SetActive(false);
            sixOptions.gameObject.SetActive(false);
        }

        public void ShowOptions(List<OptionLoin> options, float sc=2)
        {
            StartCoroutine(ShowOptionCorotine(options, sc));
        }
        private IEnumerator ShowOptionCorotine(List<OptionLoin> options , float sc)
        {
            yield return new WaitForSeconds(sc);
            SetUI(options);
        }

        private void SetUI(List<OptionLoin> options)
        {
            HideAll();
            if (options.Count == 2)
            {
                currOptions = twoOptions;
                twoOptions.gameObject.SetActive(true);
                twoOptions.SetUpOptions(options);
            }
            else if (options.Count == 3)
            {
                currOptions = treeOptions;
                treeOptions.gameObject.SetActive(true);
                treeOptions.SetUpOptions(options);
            }
            else if (options.Count == 4)
            {
                currOptions = foorOptions;
                foorOptions.gameObject.SetActive(true);
                foorOptions.SetUpOptions(options);
            }
            else if (options.Count == 5)
            {
                currOptions = fiveOptions;
                fiveOptions.gameObject.SetActive(true);
                fiveOptions.SetUpOptions(options);
            }
            else if (options.Count == 6)
            {
                currOptions = sixOptions;
                sixOptions.gameObject.SetActive(true);
                sixOptions.SetUpOptions(options);
            }
        }

        private void PlaySound(AudioClip clip)
        {
            this.UiAudioSource.clip=clip;
            this.UiAudioSource.Play();
        }
    }
}
