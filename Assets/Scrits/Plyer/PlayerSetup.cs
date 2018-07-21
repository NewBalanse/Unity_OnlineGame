using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentToDisable;
    [SerializeField]
    Camera SceneCamera;
    [SerializeField]
    string remoteLayerName = "RemotePLayer";

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
        }

        RegisterPlayer();
    }

    void RegisterPlayer()
    {
        string _id = "Player: " + GetComponent<NetworkIdentity>().netId;
        transform.name = _id;
    }

    private void OnDisable()
    {
        if(SceneCamera != null)
        {
            SceneCamera.gameObject.SetActive(true);
        }
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
}
