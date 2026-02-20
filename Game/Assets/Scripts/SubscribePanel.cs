using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SubscribePanel : MonoBehaviour
{
    [SerializeField] InputField[ ] inputFields = new InputField[3];

    private void Awake()
    {
        inputFields = GetComponentsInChildren<InputField>();
    }

    public void Subscribe()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = inputFields[0].text,
            Email = inputFields[1].text,
            Password = inputFields[2].text,
        }; 

        PlayFabClientAPI.RegisterPlayFabUser
        (
            request,
            Success,
            Failure
        );
    }

        

    public void Success(RegisterPlayFabUserResult registerPlayFabUserResult)
    {
        gameObject.SetActive(false);
    }

    public void Failure(PlayFabError playFabError)
    {
        var content = playFabError.GenerateErrorReport();

        var lines = content.Split("\n");

        PanelManager.Instance.Load(Panel.Error, $"{lines[2]} \n\n {lines[3]}");
    }
}
