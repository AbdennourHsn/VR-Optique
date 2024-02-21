using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CleanImplementation
{
    public class TestManager : MonoBehaviour
    {
        public FiltrePanel filtrePanel;
        public BoxOfColors box;
        public UiManager ui;
        public AudioSource audioSource;

        public Test[] tests;
        public VisionLoin currQuestion;
        private int CurrTest=0;

        public void Start()
        {
            ChangeQuestion(tests[CurrTest].visions[0]);
        }

        public void ChangeQuestion(VisionLoin question)
        {
            ui.HideAll();
            filtrePanel.SetupFiltre(question);
            box.SetBox(question.LB);
            RunAudioClip(question.Audio);
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

                var no = tests[CurrTest].verifie.Options[1];
                no.next = tests[CurrTest].visions.First(t => t.theMainQuestion == true);
                tests[0].verifie.Options[1] = no;

                RunAudioClip(tests[0].verifie.Audio);
                ui.ShowOptions(tests[0].verifie.Options);
            }
            else
            {
                print("We Done : resultat : " + result);
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
