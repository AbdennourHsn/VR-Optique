using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class User
{
    public int id;
    public string test_Identifier_login;
    public string firstname;
    public string lastname;
    public string userName;
    public string gender;
    public string birthDate;
    public bool eye_pathologies;
    public bool visual_defect;
    public string defects;
    public bool glasses;
    public bool isAdmin;
}

[System.Serializable]
public class TestDto
{
    public string name { get; set; }
    public bool isDone { get; set; }
    public int results { get; set; }
}


public class ServerConnection : MonoBehaviour
{
        public static ServerConnection instance;
        public string apiUrl;
        public delegate void ServerConnected(User user);
        public static ServerConnected OnserverConnected;
        public delegate void ServerFainToConnect();
        public static ServerFainToConnect OnServerFainToConnect;

    public delegate void SaveResultat(TestDto dto);
    public static SaveResultat OnSaveResultt;


    private void OnEnable()
    {
        OnSaveResultt += SaveResults;
    }

    private void OnDisable()
    {
        OnSaveResultt -= SaveResults;
    }

    private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        DontDestroyOnLoad(instance);
        }


    private void Start()
    {

    }

    public IEnumerator GetUserData(string identifier)
        {
            UnityWebRequest request = UnityWebRequest.Get(apiUrl+ "user/identifier/" + identifier);
            yield return request.SendWebRequest();

            // Check for errors
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("API request error: " + request.error);
                OnServerFainToConnect?.Invoke();
        }
            else
            {
                string responseJson = request.downloadHandler.text;
                User user = JsonUtility.FromJson<User>(responseJson);
                if (user != null)
                {
                    PlayerPrefs.SetString("UserIdentifier", identifier);
                    OnserverConnected?.Invoke(user);
                    Debug.Log(responseJson);
                    Debug.Log(user.firstname);
            }

                else
                {
                OnServerFainToConnect?.Invoke();
                    Debug.LogWarning("User not found");
                }
            }
        }


    public void SaveResults(TestDto testDto)
    {
        StartCoroutine(SetTestResultat(testDto));
    }

    public IEnumerator SetTestResultat(TestDto testDto)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", testDto.name);
        form.AddField("isDone", testDto.isDone.ToString());
        form.AddField("results", testDto.results);
        UnityWebRequest www = UnityWebRequest.Post($"{apiUrl}Test/setResultat/{PlayerPrefs.GetString("UserIdentifier")}", form);
        www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }


}
