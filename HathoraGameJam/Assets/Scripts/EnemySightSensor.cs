using System.Collections;
using System.Collections.Generic;
using FSM;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySightSensor : NetworkBehaviour
{

    private float CoolDown = -1;
    public Transform Player;

    void Update()
    {
        if (!IsServer && !IsHost)
        {
            return;
        }
        if (OnCoolDown())
        {
            CoolDown -= Time.deltaTime;
            return;
        }

        var players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        var playerInRange = players.Find((player) => player != null && Vector3.Distance(player.transform.position, transform.position) < 10);
        Player = playerInRange?.transform;
    }

    public bool Ping()
    {
        return Player != null;
    }

    public bool Caught()
    {
        return Player != null && Vector3.Distance(Player.transform.position, transform.position) < 3;
    }

    public bool OnCoolDown()
    {
        return CoolDown > 0;
    }

    public void StartCoolDown()
    {
        CoolDown = 10;
        Player = null;
    }
}
