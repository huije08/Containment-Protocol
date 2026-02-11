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

    public void Failure(PlayFabError playFabError)
    {
        Debug.Log(playFabError.GenerateErrorReport());
    }

    public void Subscribe()
    {
        Debug.Log("Create Account");
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
    
}
