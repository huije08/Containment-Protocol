using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> transforms = new List<Transform>();

    private void Awake()
    {
        SetTransform();
    }
    void Start()
    {
        Create();
    }

    public void Create()
    {
        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        PhotonNetwork.Instantiate("Character", transforms[index].position, Quaternion.identity);
    }
    
    public void SetTransform()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            Transform prefab = Instantiate(Resources.Load<Transform>("Create Position" + i));

            transforms.Add(prefab);
        }
    }
    
}
