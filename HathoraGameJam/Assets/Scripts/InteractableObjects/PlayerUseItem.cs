using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

 
public class PlayerUseItem : NetworkBehaviour
{
    [SerializeField] GameObject confettiCannonProjectile;
    PlayerInventory playerInventory;
 
    bool canAttack;
    private KeyCode keyToPress = KeyCode.Space;

    void Start()
    {
        canAttack = false;
        playerInventory = GetComponent<PlayerInventory>();
    }
     
    void Update()
    {

        if (!IsOwner)
        {
            return;
        }

        if (playerInventory.isHoldingItem)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if (canAttack)
        {
            if (Input.GetKeyDown(keyToPress))
            {

                Debug.Log("Perform Attack");

                PerformAttack(playerInventory.itemName);
                playerInventory.RemoveItem(playerInventory.itemName);
            }
        }

    


    }

    private void PerformAttack(string itemName)
    {
        switch (itemName)
        {

            case "RubberBand":
                Attack();
                break;
            case "PaperAirplane":
                Attack();
                break;
            case "StickyNote":
                Attack();
                break;
            case "DeskDrawerBarricade":
                Attack();
                break;
            case "ConfettiCannon":
                ConfettiAttack();
                break;
            case "OfficeSupplyFrenzy":
                Attack();
                break;

        }
    }

    public float confettiCannonProjectileShootForce = 10.0f;
    private void ConfettiAttack()
    {
 
        ConfettiAttackServerRpc(transform.position, transform.rotation);
    }


    Transform confettiProjectile;

    [ServerRpc(RequireOwnership = false)]
    private void ConfettiAttackServerRpc(Vector3 position, Quaternion rotation, ServerRpcParams serverRpcParams = default)
    {
        confettiProjectile = Instantiate(confettiCannonProjectile.transform, position, rotation);//, position, rotation);
        NetworkObject confettiProjectileNetworkObject = confettiProjectile.GetComponent<NetworkObject>();
        confettiProjectileNetworkObject.Spawn();
        confettiProjectileNetworkObject.GetComponent<CollisionConfettiAttackEffect>().ClientId = serverRpcParams.Receive.SenderClientId;
        ConfettiAttackClientRpc(confettiProjectileNetworkObject, serverRpcParams.Receive.SenderClientId);
    }

    private static NetworkObject GetCurrenPlayer()
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        return networkManager.GetComponent<NetworkManager>().LocalClient.PlayerObject;
    }

    [ClientRpc]
    private void ConfettiAttackClientRpc(NetworkObjectReference CollisionConfettiAttackEffectGO, ulong clientId)//CollisionConfettiAttackEffectGO
    {
        if(clientId== GetCurrenPlayer().OwnerClientId)
        {
            return;
        }


        CollisionConfettiAttackEffectGO.TryGet(out NetworkObject CollisionConfettiAttackEffectGONetworkObject);
        CollisionConfettiAttackEffect collisionConfettiAttackEffect = CollisionConfettiAttackEffectGONetworkObject.GetComponent<CollisionConfettiAttackEffect>();
        if (collisionConfettiAttackEffect==null)
        {
            // Parent already spawned an object
            return;
        }

        collisionConfettiAttackEffect.canBlind = true;


    }

    private void Attack()
    {
        Debug.Log("ImplementAttack");
    }
}
