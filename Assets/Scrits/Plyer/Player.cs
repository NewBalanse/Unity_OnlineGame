using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    int Health = 100;
    [SerializeField]
    Behaviour[] disableComp;
    bool[] wasEnabled;

    [SyncVar]
    int currentHealth;
    [SyncVar]
    bool isDead = false;

    public bool _isDead
    {
        get { return isDead; }
        protected set { isDead = value; }
    }

    public void Setup()
    {
        wasEnabled = new bool[disableComp.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableComp[i].enabled;
        }
        SetDefault();
    }

    //Speshial for testing ...

    //private void Update()
    //{
    //    if (!isLocalPlayer)
    //        return;
       
    //    if (Input.GetKeyDown(KeyCode.K))
    //        RpcTakeDamage(99999);
    //}

    [ClientRpc]
    public void RpcTakeDamage(int damage)
    {
        if (isDead)
            return;
        if (currentHealth > 0)
            currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
        Debug.Log(transform.name + "now has " + currentHealth + " health!");
    }

    public void SetDefault()
    {
        isDead = false;

        for (int i = 0; i < disableComp.Length; i++)
        {
            disableComp[i].enabled = wasEnabled[i];
        }
        currentHealth = Health;

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }

    void Die()
    {
        GameManager.instance.setiings.CountDeath++;
        isDead = true;
        //disable component ...
        for (int i = 0; i < disableComp.Length; i++)
        {
            disableComp[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;
        //respan ...
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        if (GameManager.instance.setiings.CountDeath > 0)
            GameManager.instance.setiings.respawntTime *= GameManager.instance.setiings.CountDeath;

        Debug.Log("Time  : " + GameManager.instance.setiings.respawntTime);

        yield return new WaitForSeconds(GameManager.instance.setiings.respawntTime);
        SetDefault();
        Transform startPosition = NetworkManager.singleton.GetStartPosition();
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
    }
}
