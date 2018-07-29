using System;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentToDisable;
    [SerializeField]
    Camera SceneCamera;
    [SerializeField]
    string remoteLayerName = "RemotePLayer";
    [SerializeField]
    string DontDrawlayer = "DontDraw";
    [SerializeField]
    GameObject playerGraphics;
    [SerializeField]
    GameObject playerUIPrefabs;

    GameObject playerUIInstance;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponent();
            RemotePlayer();
        }
        else
        {
            SceneCamera = Camera.main;
            if (SceneCamera != null)
            {
                SceneCamera.gameObject.SetActive(false);
            }

            //Disable player graphics
            SetLayerRec(playerGraphics, LayerMask.NameToLayer(DontDrawlayer));

            //Create player UI ...
            playerUIInstance = Instantiate(playerUIPrefabs);
            playerUIInstance.name = playerUIPrefabs.name;
        }

        GetComponent<Player>().Setup();
    }

    private void SetLayerRec(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform item in obj.transform)
        {
            SetLayerRec(item.gameObject, layer);
        }
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);
        if(SceneCamera != null)
        {
            SceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }

    void RemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponent()
    {
        for (int i = 0; i < componentToDisable.Length; i++)
        {
            componentToDisable[i].enabled = false;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _NetId = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayers(_NetId,_player);
    }
}
