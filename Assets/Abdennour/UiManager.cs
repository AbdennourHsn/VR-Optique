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

        [Space(10)]
        public GameObject canvas;
        public GameObject TestResults;
        public MeshRendererHandler All;

        private MenuOptions currOptions;

        [Space(10)]
        [Header("Navigations settings")]

        [SerializeField]
        private AudioSource UiAudioSource;

        [SerializeField]
        AudioClip slide, ok;

        public PlayerInput inputHandler;

        private bool inputActivated;
        InputAction ShowResults;
        InputAction toggleMeshes;

        private void Awake()
        {
            if (inputHandler != null)
            {
                InputAction Navigation = inputHandler.actions["Navigation"];
                InputAction select = inputHandler.actions["saveAnswer"];
                InputAction back = inputHandler.actions["backToPrevious"];


                ShowResults = inputHandler.actions["ShowResults"];
                toggleMeshes = inputHandler.actions["ToggleMesh"];
                
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

                if (ShowResults != null)
                {
                    ShowResults.started += ShowResults_;
                    ShowResults.canceled += HideResults_;
                }
                else
                {
                    Debug.LogError("ShowResults action not found!");
                }
                if (back != null)
                {
                    back.performed +=BackToPrevious;
                }
                else
                {
                    Debug.LogError("ShowResults action not found!");
                }

                if (toggleMeshes != null)
                {
                    toggleMeshes.started += ShowMeshes_;
                    toggleMeshes.canceled += HideMeshes_;
                }
                else
                {
                    Debug.LogError("ShowResults action not found!");
                }
            }
            else
            {
                Debug.LogError("PlayerInput component not found!");
            }
        }

        private void ChangeOption(InputAction.CallbackContext ctx)
        {
            if (!inputActivated) return;
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

        public void BackToPrevious(InputAction.CallbackContext ctx)
        {
            if (!inputActivated) return;
            TestManager.instance.PrevieosQuestion();
        }

        private void Choose(InputAction.CallbackContext ctx)
        {
            if (!inputActivated) return;
            PlaySound(ok);
            currOptions.menuElements[currOptions.selectedElement].Trigger();
        }

        private void ShowMeshes_(InputAction.CallbackContext ctx)
        {
            {
                print("On");
                All.SetActiveMeshes(false);
            }
        }

        private void HideMeshes_(InputAction.CallbackContext ctx)
        {
            {
                print("off");
                All.SetActiveMeshes(true);
            }
        }

        public void ShowResults_(InputAction.CallbackContext ctx)
        {
            
            {
                All.SetActiveMeshes(false);
                TestResults.SetActive(true);
            }
        }

        public void HideResults_(InputAction.CallbackContext ctx)
        {
            
            {
                All.SetActiveMeshes(true);
                TestResults.SetActive(false);
            }
        }

        private void Update()
        {
            //var value = ShowResults.ReadValue<float>();
            //if (value == 1)
            //{
            //    TestResults.SetActive(true);
            //}
            //else
            //{
            //    TestResults.SetActive(false);
            //}
        }

        public void HideAll()
        {
            inputActivated = false;
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
            inputActivated = true;
            //All.SetActiveMeshes(false);
        }

        private void PlaySound(AudioClip clip)
        {
            this.UiAudioSource.clip=clip;
            this.UiAudioSource.Play();
        }
    }
}
