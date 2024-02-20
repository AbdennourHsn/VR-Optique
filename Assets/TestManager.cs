using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CleanImplementation
{
    public class TestManager : MonoBehaviour
    {
        public FiltrePanel filtrePanel;
        public BoxOfColors box;
        public UiManager ui;

        public VisionLoin justForTest;

        public void Start()
        {
            filtrePanel.SetupFiltre(justForTest);
            box.SetBox(justForTest.LB);
            ui.SetUI(justForTest.Options);
        }
    }
}
