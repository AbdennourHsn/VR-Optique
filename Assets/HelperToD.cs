using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class HelperToD : MonoBehaviour
{
    public List<QA> QAs;


    public void Start()
    {
        StartCoroutine(Count());

    }

    IEnumerator Count()
    {
        foreach (QA qa in QAs)
        {
            Assigne(qa);
        }
        yield return new WaitForSeconds(1);
    }

    public void Assigne(QA qa)
    {
        print("Im here");
        foreach (option op in qa.options)
        {
            op.NextQestion = QAs.FirstOrDefault(q => q.id == op.nextQ);
        }
        string existingPath = "Assets/ScriptableObjects/"+ qa.id + "." + qa.name + ".asset";

        // Save the changes in the same folder
        EditorUtility.SetDirty(qa);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
}
