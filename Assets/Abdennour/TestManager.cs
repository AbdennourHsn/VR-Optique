using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using System;

namespace CleanImplementation
{
    public class TestManager : MonoBehaviour
    {
        public int visionNumber;
        public static TestManager instance;
        [Header("Dependecies")]
        public FiltrePanel filtrePanel;
        public BoxOfColors box;
        public UiManager ui;
        public AudioSource audioSource;
        public AudioClip helper;

        public Sprite Non;
        public Sprite Non_s;

        [Space(20)]
        [Header("Tests")]
        public Test[] tests;
        private Vision currQuestion;
        private int CurrTest=0;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnDisable()
        {
            instance = null;
        }

        public void Start()
        {
            filtrePanel = transform.gameObject.GetComponentInChildren<FiltrePanel>();
            StartCoroutine(NextTest(CurrTest));
        }


        private IEnumerator NextTest(int index)
        {
            filtrePanel.ShowTestName(tests[index].visions[0].Testname);
            yield return new WaitForSeconds(2);
            filtrePanel.HideTestName();
            ChangeQuestion(tests[index].visions[0]);
        }

        public void ChangeQuestion(Vision question)
        {
            StartCoroutine(ChangeQuestionCourotine(question));
        }

        public IEnumerator ChangeQuestionCourotine(Vision question)
        {
            ui.HideAll();
            filtrePanel.SetupFiltre(question);
            box.SetBox(question.LB, question.xPos , question.dots);
            if (question.helper)
            {
                RunAudioClip(helper);
                yield return new WaitForSeconds(2);
            }
            yield return new WaitForSeconds(1);
            RunAudioClip(question.Audio);
            yield return new WaitForSeconds(question.Audio.length*0.8f);
            ui.ShowOptions(question.Options);
            currQuestion = question;
        }

        private void RunAudioClip(AudioClip clip)
        {
            audioSource.Pause();
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void VerifieQuestion(Results result)
        {
            ui.HideAll();
            if (!tests[CurrTest].isVerified)
            {
                tests[CurrTest].isVerified = true;
                TestSaver.instance.SendDataToServer(tests[CurrTest].name, result, false);

                //Oui Option
                var oui = tests[CurrTest].verifie.Options[0];
                oui.ResultsCode = result;
                oui.isLast=true;
                tests[CurrTest].verifie.Options[0] = oui;

                ///////////////////////////
                //Non Option
                var no = tests[CurrTest].verifie.Options[1];
                no.next = MainQuestionVerification( tests[CurrTest].visions.First(t => t.theMainQuestion == true));
                tests[0].verifie.Options[1] = no;

                RunAudioClip(tests[0].verifie.Audio);
                ui.ShowOptions(tests[0].verifie.Options);
                ResultsTable.instance.AddResults(tests[CurrTest].name, result, false);
            }
            else
            {
                print("We Done: Test" + tests[CurrTest].name + " : resultat : " + result);
                TestSaver.instance.SendDataToServer(tests[CurrTest].name, result, true);
                ResultsTable.instance.AddResults(tests[CurrTest].name, result, true);
                if (CurrTest < tests.Length - 1)
                {
                    CurrTest += 1;
                    StartCoroutine(NextTest(CurrTest));
                }
                else
                {
                    Manager.OnTestDone?.Invoke(visionNumber);
                }
            }
        }

        private Vision MainQuestionVerification(Vision main)
        {
            if(main is VisionLoin mainQuestion)
            {
                VisionLoin vision = new VisionLoin(mainQuestion);
                vision.Options.Add(NsOption());
                vision.SetHoles(mainQuestion);
                return vision;
            }
            else
            {
                VisionIntermediate vision = new VisionIntermediate(main);
                vision.Options.Add(NsOption());
                vision.SetHoles(main);
                return vision;
            }
        }


        public OptionLoin NsOption()
        {
            return new OptionLoin
            {
                Label = "NS",
                img = Non,
                imgSelected = Non_s,
                isLast = true,
                ResultsCode = Results.NS
            };
        }
    }

    [System.Serializable]
    public struct Test
    {
        public string name;
        public Vision[] visions;
        public Vision verifie;
        public bool isVerified;
    }
}
