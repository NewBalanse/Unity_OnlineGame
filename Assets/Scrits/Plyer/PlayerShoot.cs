using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";
    public Weapon weapon;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
            Debug.LogError("Error script 'player shoot' camera not found!");
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit _hit;

        if (Physics.Raycast(cam.transform.position,
            cam.transform.forward, out _hit, weapon.range, mask))
        {
            //do something
            if (_hit.collider.tag.Equals(PLAYER_TAG))
            {
                CMDPlayerShoot(_hit.collider.name);
            }
        }
    }

    [Command]
    void CMDPlayerShoot(string _id)
    {
        //comand for server
        Debug.Log(_id + " - has been shoot!");

        Destroy(GameObject.Find(_id));
    }
}
