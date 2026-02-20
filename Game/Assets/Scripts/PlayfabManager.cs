using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField addressInputField;
    [SerializeField] InputField passwordInputField;
    [SerializeField] string version;

    public void Success(LoginResult loginResult)
    {

        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = version;
        StartCoroutine(ConnectRoutine());
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    private IEnumerator ConnectRoutine()
    {
        // Name Server에서 Mastert Server로 넘어가는 중..
        PhotonNetwork.ConnectUsingSettings();

        while (PhotonNetwork.IsConnectedAndReady == false)
        {
            yield return null;
        }

        PhotonNetwork.JoinLobby();
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = addressInputField.text,
            Password = passwordInputField.text

        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            Success,
            Failure
        );
    }

    public void Subscribe()
    {
        PanelManager.Instance.Load(Panel.Subscribe);
    }

    public void Failure(PlayFabError playFabError)
    {

        var content = playFabError.GenerateErrorReport();
        var lines = content.Split('\n');

        switch (lines.Length)
        {
            case 2:
                PanelManager.Instance.Load(Panel.Error, $"{lines[1]}");
                break;
            case 3:
                PanelManager.Instance.Load(Panel.Error, $"{lines[1]} \n\n {lines[2]}");
                break;
            case 4:
                PanelManager.Instance.Load(Panel.Error, $"{lines[2]} \n\n {lines[3]}");
                break;
            case 5:
                PanelManager.Instance.Load(Panel.Error, $"{lines[2]} \n\n {lines[3]} \n\n {lines[4]}");
                break;

        }
        PanelManager.Instance.Load(Panel.Error, playFabError.GenerateErrorReport());
        Debug.Log(playFabError.GenerateErrorReport());
    }

}
