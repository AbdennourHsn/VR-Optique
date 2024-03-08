using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestSaver : MonoBehaviour
{
    public static TestSaver instance;
    public delegate void TestSaved(string name , Results results , bool verified);
    public static TestSaved OnTestSaved;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


    public void SendDataToServer(string name, Results result , bool verified)
    {
        if (ServerConnection.instance == null) return;
        TestDto dto = new TestDto
        {
            name = name,
            isDone = true,
            results = (int)result,
            verified = verified
        };
        //OnTestSaved?.Invoke(name, result, verified);
        ServerConnection.OnSaveResultt?.Invoke(dto);
    }

    public Results filtreResult(string resultStr)
    {
        if (resultStr == "COR") return Results.Correct;
        else if (resultStr == "HR") return Results.Hyper;
        else if (resultStr == "HO") return Results.Hypo;
        else if (resultStr == "HR/SUP") return Results.Hyper_Sup;
        else if (resultStr == "HO/SUP") return Results.Hypo_Sup;
        else if (resultStr == "HR/INV") return Results.Hyper_Inversion;
        else if (resultStr == "HO/INV") return Results.Hypo_Inversion;
        else if (resultStr == "HR/DP") return Results.Hyper_Diplopie;
        else if (resultStr == "HO/DP") return Results.Hypo_Diplopie;
        else if (resultStr == "HR/SUP/DP") return Results.Hyper_Sup_Diplopie;
        else if (resultStr == "HO/SUP/DP") return Results.Hypo_Sup_Diplopie;
        else if (resultStr == "HR/MDP") return Results.Hyper_Max_Diplopie;
        else if (resultStr == "HO/MDP") return Results.Hypo_Max_Diplopie;
        else if (resultStr == "HR/INV/DP") return Results.Hyper_Inversion_Diplopie;
        else if (resultStr == "HO/INV/DP") return Results.Hypo_Inversion_Diplopie;
        else if (resultStr == "COR/INH+") return Results.Inhibitions_10p;
        else if (resultStr == "COR/INH") return Results.Inhibitions_10m;
        else if (resultStr == "NTR") return Results.NT_Right;
        else if (resultStr == "NTL") return Results.NT_Left;
        else if (resultStr == "NS") return Results.NS;
        else return Results.No_results;
    }

    public string filtreTest(string name)
    {
        if (name == "Vision De Loin Test N 1") return "B/VL/CT/4/040";
        else if (name == "Vision De Loin Test N 2") return "B/VL/CT/10/343";
        else if (name == "Vision De Loin Test N 3") return "B/VL/CT/4/121";
        else if (name == "Vision De Loin Test N 4") return "B/VL/CT/3/111";
        else if (name == "Vision De Loin Test N 5") return "B/VL/NB/4/040";
        else if (name == "Vision De Loin Test N 6") return "B/VL/NB/10/343";
        else if (name == "Vision De Loin Test N 7") return "B/VL/NB/4/121";
        else if (name == "Vision De Loin Test N 8") return "B/VL/NB/3/111";

        else if (name == "Vision intermédiaire Test N 9") return "B/VI/CT/4/040";
        else if (name == "Vision intermédiaire Test N 10") return "B/VI/CT/10/343";
        else if (name == "Vision intermédiaire Test N 11") return "B/VI/CT/4/121";
        else if (name == "Vision intermédiaire Test N 12") return "B/VI/NB/4/040";
        else if (name == "Vision intermédiaire Test N 13") return "B/VI/NB/10/343";
        else if (name == "Vision intermédiaire Test N 14") return "B/VI/NB/4/121";

        else if (name == "Vision de prés Test N 15") return "B/VP/CT/4/040";
        else if (name == "Vision de prés Test N 16") return "B/VP/CT/10/343";
        else if (name == "Vision de prés Test N 17") return "B/VP/CT/4/121";
        else if (name == "Vision de prés Test N 18") return "B/VP/CT/3/111";
        else if (name == "Vision de prés Test N 19") return "B/VP/NB/4/040";
        else if (name == "Vision de prés Test N 20") return "B/VP/NB/10/343";
        else if (name == "Vision de prés Test N 21") return "B/VP/NB/4/121";
        else if (name == "Vision de prés Test N 22") return "B/VP/NB/3/111";
        else return "";

    }

}

public enum Results
{
    No_results,
    Correct, //1- Corrigé

    Hyper, //2- Hyper
    Hypo, //3- Hypo

    Hyper_Sup, //4- Hyper Sup
    Hypo_Sup, //5- Hypo Sup

    Hyper_Inversion, //6- Hyper Inversion
    Hypo_Inversion,  //7- Hypo Inversion

    Hyper_Diplopie,  //8- Hyper Diplopie
    Hypo_Diplopie, //9- Hypo Diplopie

    Hyper_Sup_Diplopie, //10- Hyper Sup Diplopie
    Hypo_Sup_Diplopie,  //11- Hypo Sup Diplopie

    Hyper_Max_Diplopie, //12- Hyper Max Diplopie
    Hypo_Max_Diplopie,  //13- Hypo Max Diplopie

    Hyper_Inversion_Diplopie, //14- Hyper Inversion Diplopie
    Hypo_Inversion_Diplopie, //15- Hypo Inversion Diplopie


    Inhibitions_10p, //16- Inhibitions + de 10
    Inhibitions_10m, //17- Inhibitions – de 10

    NT_Right, //18- NT Right
    NT_Left, //19- NT Left
    NS, // Non stable
    NT, //NT
}