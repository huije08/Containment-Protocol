using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class GeneratorPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField InputField;

    [SerializeField] Toggle [ ] toggles;
    [SerializeField] int personnal = 0;

    void Start()
    {
        Select(true);
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = personnal;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.CreateRoom(InputField.text, roomOptions);
        gameObject.SetActive(false);
    }

    public void Select(bool state)
    {
        if (state == false)
        {
            return;
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                personnal = i + 2;
                break;
            }
        }
    }

}
