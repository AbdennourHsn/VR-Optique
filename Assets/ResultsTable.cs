using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ResultsTable : MonoBehaviour
{
    public Transform group;
    public List<Card> card= new List<Card>();

    public static ResultsTable instance;

    private void OnEnable()
    {
        TestSaver.OnTestSaved += AddResults;
    }

    private void OnDisable()
    {
        TestSaver.OnTestSaved -= AddResults;
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddResults(string name , Results results , bool verified)
    {
        var card = GetCardByName(name);
        if (!verified)
            card.resultat.text = results.ToString();
        else card.order.text = results.ToString();
    }

    public Card GetCardByName(string name)
    {
        return card.FirstOrDefault(c => c.name == name);
    }

}
