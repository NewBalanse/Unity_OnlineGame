using System;
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
                CmdPlayerShoot(_hit.collider.name, weapon.damage);
            }
        }
    }
    
    [Command]
    void CmdPlayerShoot(string _id, int damage)
    {
        try
        {
            //comand for server
            Debug.Log(_id + " - has been shoot!");

            Player player = GameManager.GetPlayer(_id);

            player.RpcTakeDamage(damage);
        }
        catch (Exception e)
        {
            Debug.Log("Exception is script PlayerShoot at methods C_PlayerShoot!" + e.Message);
        }
        
    }
}
