using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

namespace CleanImplementation
{
    public class TestManager : MonoBehaviour
    {
        [Header("Dependecies")]
        public FiltrePanelVL filtrePanel;
        public BoxOfColors box;
        public UiManager ui;
        public AudioSource audioSource;
        public AudioClip helper;

        [Space(20)]
        [Header("Tests")]
        public Test[] tests;
        private VisionLoin currQuestion;
        private int CurrTest=0;

        public void Start()
        {
            //ChangeQuestion(tests[CurrTest].visions[0]);
            StartCoroutine(NextTest(CurrTest));
        }


        private IEnumerator NextTest(int index)
        {
            filtrePanel.ShowTestName(tests[index].visions[0].Testname);
            yield return new WaitForSeconds(2);
            filtrePanel.HideTestName();
            ChangeQuestion(tests[index].visions[0]);
        }

        public void ChangeQuestion(VisionLoin question)
        {
            StartCoroutine(ChangeQuestionCourotine(question));
        }

        public IEnumerator ChangeQuestionCourotine(VisionLoin question)
        {
            ui.HideAll();
            filtrePanel.SetupFiltre(question);
            box.SetBox(question.LB, question.xPos);
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
                var oui = tests[CurrTest].verifie.Options[0];
                oui.ResultsCode = result;
                oui.isLast=true;
                tests[CurrTest].verifie.Options[0] = oui;

                ///////////////////////////
                var no = tests[CurrTest].verifie.Options[1];
                no.next = tests[CurrTest].visions.First(t => t.theMainQuestion == true);
                tests[0].verifie.Options[1] = no;

                RunAudioClip(tests[0].verifie.Audio);
                ui.ShowOptions(tests[0].verifie.Options);
            }
            else
            {
                print("We Done : resultat : " + result);
                CurrTest += 1;
                StartCoroutine(NextTest(CurrTest));
            }
        }
    }

    [System.Serializable]
    public struct Test
    {
        public string name;
        public VisionLoin[] visions;
        public VisionLoin verifie;
        public bool isVerified;
    }
}
