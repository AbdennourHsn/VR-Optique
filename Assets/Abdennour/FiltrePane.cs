using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FiltrePanel
{
    public void SetupFiltre(Vision vision);
    public void HideAll();
    public void ShowAll();
    public void ShowTestName(string name);
    public void HideTestName();
}
