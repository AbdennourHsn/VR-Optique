using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Card : MonoBehaviour
{
    public string TestName;
    public TextMeshProUGUI test;
    public TextMeshProUGUI resultat;
    public TextMeshProUGUI order;

    private void Start()
    {
        test.text = gameObject.name;
    }
}
