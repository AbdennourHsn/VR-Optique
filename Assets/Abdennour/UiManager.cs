using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CleanImplementation
{
    public class UiManager : MonoBehaviour
    {
        [Header("Menu Options Plaque filtre")]
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

        [Header("Menu Options")]
        [SerializeField]
        private MenuOptions twoOptions_;
        [SerializeField]
        private MenuOptions treeOptions_;
        [SerializeField]
        private MenuOptions foorOptions_;
        [SerializeField]
        private MenuOptions fiveOptions_;
        [SerializeField]
        private MenuOptions sixOptions_;

        [Space(10)]
        public GameObject canvas;
        public GameObject TestResults;
        public MeshRendererHandler All;

        [Space(10)]
        public TextMeshProUGUI messageArea;

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

        public delegate void SetMessage(string message);
        public static SetMessage OnSetMessage;

        private void OnEnable()
        {
            OnSetMessage += SetMessageText;
        }

        private void OnDisable()
        {
            OnSetMessage -= SetMessageText;
        }

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
            SetMessageText("");
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

        public void SetMessageText(string message)
        {
            this.messageArea.text = message;
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
            twoOptions_.gameObject.SetActive(false);
            treeOptions_.gameObject.SetActive(false);
            foorOptions_.gameObject.SetActive(false);
            fiveOptions_.gameObject.SetActive(false);
            sixOptions_.gameObject.SetActive(false);
        }

        public void ShowOptions(List<OptionLoin> options , bool upOfPlaqueFilte = true , float sc = 2)
        {
            StartCoroutine(ShowOptionCorotine(options, sc , upOfPlaqueFilte));
        }
        private IEnumerator ShowOptionCorotine(List<OptionLoin> options , float sc , bool upOfPlaqueFilte=true)
        {
            yield return new WaitForSeconds(sc);
            All.SetActiveMeshes(!upOfPlaqueFilte);
            SetUI(options , upOfPlaqueFilte);
        }

        private void SetUI(List<OptionLoin> options , bool upOfPlaqueFilte=true)
        {
            HideAll();
            Dictionary<int, MenuOptions> optionUIPairs = !upOfPlaqueFilte ?
                new Dictionary<int, MenuOptions>
                {
            { 2, twoOptions },
            { 3, treeOptions },
            { 4, foorOptions },
            { 5, fiveOptions },
            { 6, sixOptions }
                } :
                new Dictionary<int, MenuOptions>
                {
            { 2, twoOptions_ },
            { 3, treeOptions_ },
            { 4, foorOptions_ },
            { 5, fiveOptions_ },
            { 6, sixOptions_ }
                };

            if (optionUIPairs.TryGetValue(options.Count, out MenuOptions selectedUI))
            {
                currOptions = selectedUI;
                selectedUI.gameObject.SetActive(true);
                selectedUI.SetUpOptions(options);
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
