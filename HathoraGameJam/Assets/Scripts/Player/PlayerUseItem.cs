using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class PlayerUseItem : NetworkBehaviour
{
    [SerializeField] GameObject confettiCannonProjectile;
    [SerializeField] GameObject stickyNoteProjectile;
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

        //if (Input.GetKeyDown(keyToPress))
        //{

        //    Debug.Log("Test  Attack");

        //    InvisibilityAttack();
        //}



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
                StickyNoteAttack();
                break;
            case "Invisibility":
                InvisibilityAttack();
                break;
            case "ConfettiCannon":
                ConfettiAttack();
                break;
            case "OfficeSupplyFrenzy":
                Attack();
                break;

        }
    }

    private void InvisibilityAttack()
    {
        GetComponent<SFX>().PlaySecondEffect();
        ChangePlayerTagServerRpc(gameObject);
    }


    [ServerRpc(RequireOwnership = false)]
    private void ChangePlayerTagServerRpc(NetworkObjectReference playerUsedItem)
    {
        playerUsedItem.TryGet(out NetworkObject player);
        StartCoroutine(SetTag(player.gameObject));
        ChangePlayerTagClientRpc(playerUsedItem);

    }


    [ClientRpc]
    private void ChangePlayerTagClientRpc(NetworkObjectReference playerUsedItem)
    {
        playerUsedItem.TryGet(out NetworkObject player);
        StartCoroutine(SetTag(player.gameObject));
    }

    float inviDuration = 5f;
    IEnumerator SetTag(GameObject player)
    {
        player.tag = "Immortal";
        yield return new WaitForSeconds(inviDuration);
        player.tag = "Player";

    }

    private void StickyNoteAttack()
    {
        StickyNoteAttackServerRpc(transform.position, transform.rotation);
    }

    public float confettiCannonProjectileShootForce = 10.0f;
    private void ConfettiAttack()
    {

        ConfettiAttackServerRpc(transform.position, transform.rotation);
    }


    Transform Projectile;

    [ServerRpc(RequireOwnership = false)]
    private void ConfettiAttackServerRpc(Vector3 position, Quaternion rotation, ServerRpcParams serverRpcParams = default)
    {
        Projectile = Instantiate(confettiCannonProjectile.transform, position, rotation);
        NetworkObject confettiProjectileNetworkObject = Projectile.GetComponent<NetworkObject>();
        confettiProjectileNetworkObject.Spawn();
        confettiProjectileNetworkObject.GetComponent<CollisionConfettiAttackEffect>().ClientId = serverRpcParams.Receive.SenderClientId;
        ConfettiAttackClientRpc(confettiProjectileNetworkObject, serverRpcParams.Receive.SenderClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void StickyNoteAttackServerRpc(Vector3 position, Quaternion rotation)
    {
        Projectile = Instantiate(stickyNoteProjectile.transform, new Vector3(position.x, 0f, position.z), stickyNoteProjectile.transform.rotation);
        NetworkObject stickyNoteNetworkObject = Projectile.GetComponent<NetworkObject>();
        stickyNoteNetworkObject.Spawn();
    }

    private static NetworkObject GetCurrenPlayer()
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        return networkManager.GetComponent<NetworkManager>().LocalClient.PlayerObject;
    }

    [ClientRpc]
    private void ConfettiAttackClientRpc(NetworkObjectReference CollisionConfettiAttackEffectGO, ulong clientId)//CollisionConfettiAttackEffectGO
    {
        if (clientId == GetCurrenPlayer().OwnerClientId)
        {
            return;
        }


        CollisionConfettiAttackEffectGO.TryGet(out NetworkObject CollisionConfettiAttackEffectGONetworkObject);
        CollisionConfettiAttackEffect collisionConfettiAttackEffect = CollisionConfettiAttackEffectGONetworkObject.GetComponent<CollisionConfettiAttackEffect>();
        if (collisionConfettiAttackEffect == null)
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
