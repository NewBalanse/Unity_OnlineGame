using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public MatchSetiings setiings;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More instance");
        }
        else {
            instance = this;
        }
    }
    #region Player register

    const string PLAYER_PREFIX_ID = "Player";

    static Dictionary<string, Player> _players = new Dictionary<string, Player>();

    public static void RegisterPlayers(string _netIdPlayer,Player player)
    {
        string _playerId = PLAYER_PREFIX_ID +_netIdPlayer;
        _players.Add(_playerId, player);
        player.transform.name = _playerId;
    }

    public static void UnRegisterPlayer(string _playerId)
    {
        _players.Remove(_playerId);
    }

    public static Player GetPlayer(string player)
    {
        return _players[player];
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string playerID in _players.Keys)
    //    {
    //        GUILayout.Label(playerID + " - " + _players[playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}
    #endregion

}
