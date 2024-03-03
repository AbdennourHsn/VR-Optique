using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsTable : MonoBehaviour
{
    public Transform group;
    public Card card;
    public GameObject Line;
    private void OnEnable()
    {
        TestSaver.OnTestSaved += AddResults;
    }

    private void OnDisable()
    {
        TestSaver.OnTestSaved -= AddResults;
    }

    public void AddResults(string name , Results results , bool verified)
    {
        if (!verified) Instantiate(Line, group).SetActive(true); ;
        var newCard=Instantiate(card, group);
        newCard.test.text = name;
        newCard.resultat.text = results.ToString();
        if (verified) newCard.order.text = "2";
        else newCard.order.text = "1";
        newCard.gameObject.SetActive(true);
    }

}
