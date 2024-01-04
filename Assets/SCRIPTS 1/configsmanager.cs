
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ConfigsManager : MonoBehaviour
{
    // Declare a collection to hold the question objects
    public static Questions Q;
    public static List<string> offsetedObjectNamesList = new List<string>();
    public static Dictionary<string, float> offsetedObjectXValueList = new Dictionary<string, float>();
    // Use this for initialization
    public static Dictionary<string, string> answers = new Dictionary<string, string>();
    public static List<int> beginTestIds = new List<int>{0,8,17,23,30,38,47,53,60,68,77,83,91,100,106,114,123,129,136,144,153,159};
    public static List<int> basicTests = new List<int>{3,4,7,8,11,14,17,18,21,22};
    public static List<int> basic4Circles = new List<int> { 3, 7, 11, 14, 17, 21 };

    public static List<string> screens = new List<string> {
        "pas de resultat",
        "pas de resultat",
        "pas du resultat",
        "pas du resultat"
    };

    static List<string> initialScreens = new List<string>();
    private static Dictionary<string, string> history = new Dictionary<string, string>();
    private void Start()
    {
        //for (int i = 1;i<=22;i++)
        //{
        //    history.Add("$"+i.ToString()+"$", "$" + i.ToString() + "$");
        //}
        //initialScreens = new List<string>(screens);
        //for (int i = 0; i < 26; i++)
        //{
        //    char character = (char)('A' + i);
        //    history.Add("$" + character + "$", "$" + character + "$");
        //}
        //history.Add("$-A$", "$-A$");
        //history.Add("$-B$", "$-B$");
        //history.Add("$-C$", "$-C$");
        //history.Add("$-1$", "$-1$");
        //history.Add("$-2$", "$-2$");
        //history.Add("$-3$", "$-3$");
        //history.Add("$-4$", "$-4$");
        //history.Add("$-5$", "$-5$");
    }

    public static void saveAnswer(string TestName, string TestResult)
    {

        //if the result exists overrid it
        if (answers.ContainsKey(TestName))
        {
            answers[TestName] = TestResult;
        }
        else
        {
            answers.Add(TestName, TestResult);
        }


        var svl = SummarizeVisionType(1, 8);
        var svl1 = SummarizeVisionType(1, 4);
        var svl2 = SummarizeVisionType(5, 8);
        screens[0] = "Vision de loin CT :\nB/VL/CT/+\n4/040/" +
            fval("Vision De Loin Test N 1")
            + " � 10/343/" +
            fval("Vision De Loin Test N 2")
            + " � 4/121/" +
            fval("Vision De Loin Test N 3")
            + " � 3/111/" +
            fval("Vision De Loin Test N 4")
            + "\nNb total tests corrig�s " + svl1[0] + " - Nb total tests d�fauts simples " + svl1[1] + " - Nb total d�fauts importants " + svl1[2] +
            "\nVision de loin LNB :\nB/VL/LNB/+\n4/040/" +
            fval("Vision De Loin Test N 5")
            + " � 10/343/" +
            fval("Vision De Loin Test N 6")
            + " � 4/121/" +
            fval("Vision De Loin Test N 7")
            + " � 3/111/" +
            fval("Vision De Loin Test N 8")
            + "\nNb total tests corrig�s " + svl2[0] + " - Nb total tests d�fauts simples " + svl2[1] + " - Nb total d�fauts importants " + svl2[2] +
            "\nCumul CT + LNB\nNb total tests corrig�s (IB) " + svl[3] + "\nNb total tests d�fauts simples (IB) " + svl[4] + "\nCumul tests corrig�s et tests d�fauts simples (IB) " + svl[5] + "\nCumul tests corrig�s et tests d�fauts simples (4C) " + svl[6] + "\nNb total tests HR " + svl[7] + "\nNb total tests HO " + svl[8] + "\n";

        var svi = SummarizeVisionType(9, 14);
        var svi1 = SummarizeVisionType(9, 11);
        var svi2 = SummarizeVisionType(12, 14);

        screens[1] = "Vision interm�diaire CT :\nB/VI/CT/+\n4/040/" +
            fval("Vision interm�diaire Test N 9")
            + " � 10/343/" +
           fval("Vision interm�diaire Test N 10")
            + " � 4/121/" +
            fval("Vision interm�diaire Test N 11")
            + "\nNb total tests corrig�s " + svi1[0] + " - Nb total tests d�fauts simples " + svi1[1] + " - Nb total d�fauts importants " + svi1[2] + "\nVision interm�diaire LNB :\nB/VI/LNB/+\n4/040/" +
            fval("Vision interm�diaire Test N 12")
            + " � 10/343/" +
            fval("Vision interm�diaire Test N 13")
            + " � 4/121/" +
            fval("Vision interm�diaire Test N 14")
            + "\nNb total tests corrig�s " + svi2[0] + " - Nb total tests d�fauts simples " + svi2[1] + " - Nb total d�fauts importants " + svi2[2] + "\nCumul CT + LNB\nNb total tests corrig�s (IB) " + svi[3] + "\nNb total tests d�fauts simples (IB)" + svi[4] + "\nCumul tests corrig�s et tests d�fauts simples (IB)" + svi[5] + "\nNb total tests HR " + svi[7] + "\nNb total tests HO " + svi[7];

        var svp = SummarizeVisionType(15, 22);
        var svp1 = SummarizeVisionType(15, 18);
        var svp2 = SummarizeVisionType(19, 22);

        screens[2] = "Vision de pr�s CT :\nB/VP/CT/+\n4/040/" +
            fval("Vision de pr�s Test N 15")
            + " � 10/343/" +
            fval("Vision de pr�s Test N 16")
            + " � 4/121/" +
            fval("Vision de pr�s Test N 17")
            + " � 3/111/" +
            fval("Vision de pr�s Test N 18")
            + "\nNb total tests corrig�s " + svp1[0] + " - Nb total tests d�fauts simples " + svp1[1] + " - Nb total d�fauts importants " + svp1[2] + "\nVision de pr�s LNB :\nB/VP/LNB/+\n4/040/" +
            fval("Vision de pr�s Test N 19")
            + " � 10/343/" +
            fval("Vision de pr�s Test N 20")
            + " � 4/121/" +
            fval("Vision de pr�s Test N 21")
            + " � 3/111/" +
            fval("Vision de pr�s Test N 22")
            + "\nNb total tests corrig�s " + svp2[0] + " - Nb total tests d�fauts simples " + svp2[1] + " - Nb total d�fauts importants " + svp2[2] + "\nCumul CT + LNB\nNb total tests corrig�s (IB) " + svp[3] + "\nNb total tests d�fauts simples (IB) " + svp[4] + "\nCumul tests corrig�s et tests d�fauts simples (IB) " + svp[5] + "\nCumul tests corrig�s et tests d�fauts simples (4C) " + svp[6] + "\nNb total tests HR " + svp[7] + "\nNb total tests HO " + svp[8];

        //distance
        var distance = SummarizeVisionType(1, 8)[0] >= 3 ? "VL" : "-";

        if (distance == "-")
        {
            if (SummarizeVisionType(15, 22)[0] > SummarizeVisionType(1, 8)[0])
            {
                distance = "VP";
            }
            else
            {
                distance = "VL";
            }
            if (SummarizeVisionType(15, 22)[0] == SummarizeVisionType(1, 8)[0])
            {
                if (SummarizeVisionType(15, 22)[0] + SummarizeVisionType(15, 22)[1] > SummarizeVisionType(1, 8)[0] + SummarizeVisionType(1, 8)[1])
                {
                    distance = "VP";
                }
            }
            else distance = "VL";
        }
        //profile & excitant
        var profile = "";
        var pHO = 0;
        var pHR = 0;
        var s3d = 0;
        var s3i = svi[3];
        var excitant = "";
        if (distance == "VL")
        {
            s3d = svl[3];
            profile = SummarizeVisionType(1, 8)[7] > SummarizeVisionType(1, 8)[8] ? "HR" : "HO";
            pHR = SummarizeVisionType(1, 8)[7];
            pHO = SummarizeVisionType(1, 8)[8];

            if (SummarizeVisionType(1, 4)[0] + SummarizeVisionType(1, 4)[1] > SummarizeVisionType(5, 8)[0] + SummarizeVisionType(5, 8)[1])
            {
                excitant = "CT";
            }
            else
            {
                excitant = "LNB";
            }


        }
        if (distance == "VP")
        {
            s3d = svp[3];
            profile = SummarizeVisionType(15, 22)[7] > SummarizeVisionType(15, 22)[8] ? "HR" : "HO";
            pHR = SummarizeVisionType(15, 22)[7];
            pHO = SummarizeVisionType(15, 22)[8];
            if (SummarizeVisionType(15, 17)[0] + SummarizeVisionType(15, 17)[1] > SummarizeVisionType(18, 22)[0] + SummarizeVisionType(18, 22)[1])
            {
                excitant = "CT";
            }
            else
            {
                excitant = "LNB";
            }
        }

        screens[3] = "distance = \n" + distance + "\n"+distance+" Cumul tests corrig�s et tests d�fauts simples = "+ s3d + "\n VI Cumul tests corrig�s et tests d�fauts simples = "+s3i+"\n\nprofile = " + profile + "\nHR = " + pHR + "\nHO = " + pHO + "\n\nl'excitant = " + excitant;
    }
    private static string fval(string key)
    {
        return answers.ContainsKey(key)  ? answers[key] : "...";
    }

    static List<int> SummarizeVisionType( int start, int end)
    {
        int correctAmmount = 0;
        int simpleDefectAmmount = 0;
        int ImoportantDefectAmmount = 0;
        int hos = 0;
        int hrs = 0;
        int corbasic = 0;
        int cor4basic = 0;
        int basicSimpleDefault = 0;
        int basicSimpleDefault4 = 0;

        foreach (string testName in ConfigsManager.answers.Keys)
        {
            int testNumber = int.Parse(testName.Split('N')[1]);
            if (testNumber >= start && testNumber <= end)
            {
                if (ConfigsManager.answers[testName].StartsWith("COR")) 
                {
                    correctAmmount++; // any correct test
                    
                }
                else if ( ConfigsManager.answers[testName] == "HO" || ConfigsManager.answers[testName] == "HR" )
                {
                    simpleDefectAmmount++; //any test with Ho or HR as result
                 
                }
                else
                {
                    ImoportantDefectAmmount++; // any thing else not HR not HO and not COR
                }

                if (ConfigsManager.answers[testName].StartsWith("HO"))
                {
                   hos++; //any results that has HO type
                }
                else if (ConfigsManager.answers[testName].StartsWith("HR"))
                {
                   hrs++; //any results that has HR type
                }

                if (basicTests.Contains(testNumber))
                {
                    if (ConfigsManager.answers[testName].StartsWith("COR"))
                    {
                        corbasic++; //count correct test if test is basic (1,2,5,6,9,10,12,13,15,16,19,20)
                    }
                    else if(ConfigsManager.answers[testName] == "HO" || ConfigsManager.answers[testName] == "HR")
                    {
                        basicSimpleDefault++;
                    }
                }
                if (basic4Circles.Contains(testNumber))
                {
                    if (ConfigsManager.answers[testName].StartsWith("COR"))//count correct test if test is basic (1,5,9,12,15,19)
                    {
                        cor4basic++;
                    }
                    else if (ConfigsManager.answers[testName] == "HO" || ConfigsManager.answers[testName] == "HR")
                    {
                        basicSimpleDefault4++;
                    }
                }

            }
        }
        List<int> stats = new List<int>();

        stats.Add(correctAmmount);
        stats.Add(simpleDefectAmmount);
        stats.Add(ImoportantDefectAmmount);
        stats.Add(corbasic);
        stats.Add(basicSimpleDefault);
        stats.Add(basicSimpleDefault+ corbasic);
        stats.Add(basicSimpleDefault4+ cor4basic);
        stats.Add(hrs);
        stats.Add(hos);


        return stats;

    }

    //public static void CreateQA(Question Q)
    //{
    //    QA qaData = ScriptableObject.CreateInstance<QA>();
    //    qaData.id = Q.id;
    //    qaData.name = Q.name;
    //    qaData.questionString = Q.questionString;
    //    qaData.hiddens = Q.hiddens;
    //    qaData.visibales = Q.visibales;
    //    qaData.options = Q.options;
    //    qaData.layouts = Q.layouts;
    //    qaData.time = Q.time;
    //    qaData.audio = Q.audio;
    //    qaData.beforAudio = Q.beforAudio;
    //    qaData.label = Q.label;
    //    string path = "Assets/ScriptableObjects/";
        
    //    // Ensure the directory exists, create it if necessary
    //    if (!Directory.Exists(path))
    //    {
    //        Directory.CreateDirectory(path);
    //    }
    //    string fileName = Q.id+"."+Q.name+".asset";
    //    string assetPath = Path.Combine(path, fileName);
    //    AssetDatabase.CreateAsset(qaData, assetPath);
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //}

    public static void LoadQuestionFromResources()
    {
        string jsonString = Resources.Load<TextAsset>("questions").text;

        // Deserialize the JSON string into a collection of Question objects
        try
        {
            Q = JsonUtility.FromJson<Questions>(jsonString);
            GameObject[] allObjects =  Resources.FindObjectsOfTypeAll<GameObject>();

            for (int i = 0; i < Q.questions.Count; i++)
            {
                //Debug.Log(i);
                Question q = Q.questions[i];
               //CreateQA(q);
               // Debug.Log("Current Q: "+q.id);
                foreach (Layout l in q.layouts)
                {
                    GameObject objs = allObjects.Where(o=>o.name==l.name).First();
                    if (objs != null)
                    {
                        if (!offsetedObjectXValueList.ContainsKey(objs.name))
                        {
                            offsetedObjectXValueList.Add(objs.name, objs.transform.position.x);

                        }
                    }

                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

}

// Declare the Question class
[System.Serializable]
public class Question
{
    public Question(QA qa)
    {
        id = qa.id;
        GroupId = qa.groupID;
        beforAudio = qa.beforAudio;
        name = qa.name;
        label = qa.label;
        questionString = qa.questionString;
        time = qa.time;
        audio = qa.audio;
        visibales = qa.visibales;
        hiddens = qa.hiddens;
        options = qa.options;
        layouts = qa.layouts;
        action = qa.action;
        keepSameImg = qa.keepSameImg;
        toBeVerified = qa.toBeVerified;
    }
    public Question(Question Q)
    {
        name = Q.name;
        id = Q.id;
        GroupId = Q.GroupId;
        questionString = Q.questionString;
        hiddens = Q.hiddens;
        visibales = Q.visibales;
        options = Q.options;
        layouts = Q.layouts;
        time = Q.time;
        audio = Q.audio;
        beforAudio = Q.beforAudio;
        label = Q.label;
        keepSameImg = Q.keepSameImg;
        toBeVerified = Q.toBeVerified;
    }
    public int id;
    public int GroupId;
    public string beforAudio;
    public string name;
    public string label;
    public string questionString;
    public float time;
    public string audio;
    public static bool isRepeated=false;
    public List<string> visibales;
    public List<string> hiddens;
    public List<Option> options;
    public List<Layout> layouts;
    public string action;
    public bool toBeVerified=false;
    public bool keepSameImg = false;

    public void UpdateValues(QA qa)
    {
        id = qa.id;
        GroupId = qa.groupID;
        beforAudio = qa.beforAudio;
        name = qa.name;
        label = qa.label;
        questionString = qa.questionString;
        time = qa.time;
        audio = qa.audio;
        visibales = qa.visibales;
        hiddens = qa.hiddens;
        options = qa.options;
        layouts = qa.layouts;
        action = qa.action;
        keepSameImg = qa.keepSameImg;
        toBeVerified = qa.toBeVerified;
    }

    public Option selectedOption()
    {
        foreach (Option opt in options)
        {
            if (opt.iSselected) return opt;
        }
        return null;
    }

    public void selectOption(int optionIndex)
    {
        foreach (Option op in options)
        {
            op.iSselected = false;
        }
        options[optionIndex].iSselected = true;
    }

    public void init()
    {
        //show visibles
        foreach (string objName in visibales)
        {
            toogleActivity(objName, true);
        }
        //hide hiddens
        foreach (string objName in hiddens)
        {
            toogleActivity(objName, false);
        }
        //layouts
        if (layouts != null && layouts.Count>0)
        {
            foreach (var item in layouts)
            {

                if (GameObject.Find(item.name)!=null)
                {
                    Vector3 newpos = GameObject.Find(item.name).transform.position;
                    newpos.x = item.value;
                    GameObject.Find(item.name).transform.position = newpos;
                }

            }
        }
        //action 

    }

    public static void toogleActivity(string objName, bool value)
    {

        GameObject go = GameObject.Find(objName);
        //if (go == null)
        //{
        //    go = GameObject.FindGameObjectWithTag(objName);
        //}
        if (go != null )
        {
            go.SetActive(value);
        }
    }
}

[System.Serializable]
public class Option
{
    public string label;
    public string image_name;
    public string image_selected;
    public int nextQ;
    public QA NextQestion;
    public string resultCode;
    public bool iSselected;
    public bool hasSameValuesWith(Option o)
    {
        return label == o.label && nextQ == o.nextQ && image_name == o.image_name && image_selected == o.image_selected;
    }
    public string get_selected_image()
    {
        if (iSselected)
        {
            return image_selected;
        }
        return image_name;
    }
    public void ToggleSelection(bool value)
    {
        iSselected = value;
    }
}
public enum MODE
{
    COLORS,
    LETTERS
}

// Declare the Layout class
[System.Serializable]
public class Layout
{
    public string name;
    public float value;

    //public Layout(string name, float value)
    //{
    //    this.name = name; this.value = value;
    //}
}

[System.Serializable]
public class Questions
{
    public List<Question> questions=new List<Question>();

    public Questions(QA[] qas)
    {
        foreach(QA qa in qas)
        {
            questions.Add(new Question(qa));
        }
    }
}

