using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class ConnectionsEvent : UnityEvent
{ }

public class NumberPanel : MonoBehaviour
{
    public Text inputfiel;

    public string number ="";
    public TextMeshProUGUI connectedTxt;

    public ConnectionsEvent connected;
    public ConnectionsEvent failToConnect;
    public ConnectionsEvent logout;

    private void OnEnable()
    {
        ServerConnection.OnserverConnected += Connected;
        ServerConnection.OnServerFainToConnect += FailToConnect;
    }

    private void OnDisable()
    {
        ServerConnection.OnserverConnected -= Connected;
        ServerConnection.OnServerFainToConnect -= FailToConnect;
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("UserIdentifier").Length==6)
        {
            StartCoroutine(ServerConnection.instance.GetUserData(PlayerPrefs.GetString("UserIdentifier")));
        }
    }

    public void setNumber(int num)
    {
        if (number.Length < 6)
        {
            number += num;
            this.inputfiel.text = number;
        }
    }

    public void DeleteNum()
    {
        if (this.number.Length > 0)
        {
           number= number.Remove(number.Length - 1);
           this.inputfiel.text = number;
        }
    }

    public void RequestServer()
    {
        if (number.Length != 0)
        {
            StartCoroutine(ServerConnection.instance.GetUserData(number));
        }
    }

    private void Update()
    {

    }

    public void Connected(User user)
    {
        connected?.Invoke();
        connectedTxt.text = user.firstname + " " + user.lastname;
        Debug.Log(user.firstname + " " + user.lastname);
    }

    public void Logout()
    {
        this.number = "";
        this.inputfiel.text = number;
        PlayerPrefs.DeleteAll();
        logout?.Invoke();
    }

    public void FailToConnect()
    {
        number = "";
        inputfiel.text = number;
        failToConnect?.Invoke();
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene("tutorial");
    }
}
