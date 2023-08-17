using UnityEngine;
using Unity.Netcode;
using System;
using Random = UnityEngine.Random;

public class GainPointsPressingSeveralTimes : NetworkBehaviour, IHasProgress
{

    ScoreboardManager scoreBoard;
    public int howMuchPointGiveThisObject;
    NetworkVariable<bool> isExhausted = new NetworkVariable<bool>();

    public Material enabledItemMaterial;
    public Material disableItemMaterial;
    [SerializeField] GameObject progressBarUI;
    [SerializeField] GameObject vfx;
    GameObject currentPlayerInteractable;

    private float timer = 0f;
    private float interval = 10f; // 10 seconds interval



    public float resetTime = 5f;
    private bool oneTime;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public override void OnNetworkSpawn()
    {
        isExhausted.OnValueChanged += isExhausted_OnValueChanged;
    }

    private void isExhausted_OnValueChanged(bool previousValue, bool newValue)
    {
        Debug.Log("isExhausted_OnValueChanged changed! several time");
    }

    private void Start()
    {
        progressBarUI.GetComponent<ProgressBarUI>().tooltipText.text = "Press R";
        isExhausted.Value = false;
        if (howMuchPointGiveThisObject == 0)
        {
            howMuchPointGiveThisObject = 20;
        }
        scoreBoard = GameObject.Find("ScoreBoardManagerCanvas").GetComponent<ScoreboardManager>();
    }


    public int keyPressCount = 0;
    public int targetKeyPresses = 5; // Number of times 'R' key needs to be pressed
    public KeyCode keyToPress = KeyCode.R; // Key you want to track
    private bool playerCanPressKey;

    private void OnTriggerStay(Collider other)
    {

        if (!oneTime)
        {
            if ((other.tag == "Player" || other.tag == "Immortal") && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
            {
                playerCanPressKey = true;
                currentPlayerInteractable = other.gameObject;
            }
        }


    }


    private void Update()
    {
        if (playerCanPressKey)
        {
            if (currentPlayerInteractable != null)
            {
                CounterKey(currentPlayerInteractable);
            }
        }
    }

    private void CounterKey(GameObject currentPlayer)
    {
        if (!isExhausted.Value)
        {

            if (Input.GetKeyDown(keyToPress))
            {
                keyPressCount++;
                IncreaseProgressBar(keyPressCount);
                if (keyPressCount >= targetKeyPresses)
                {
                    progressBarUI.SetActive(false);
                    oneTime = true;
                    IncreasePlayerPoints(currentPlayer);
                    keyPressCount = 0; // Reset the count after firing the function
                }
            }
        }
        else
        {
            progressBarUI.GetComponent<ProgressBarUI>().slider.value = 0;
            progressBarUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.GetComponent<MeshRenderer>().material == disableItemMaterial)
        {
            return;
        }
        if ((other.tag == "Player" || other.tag == "Immortal") && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {

            playerCanPressKey = true;
            progressBarUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Immortal") && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            progressBarUI.GetComponent<ProgressBarUI>().slider.value = 0;
            keyPressCount = 0;
            progressBarUI.SetActive(false);
            playerCanPressKey = false;
        }
    }

    private void IncreaseProgressBar(int KeyPressCount)
    {
        progressBarUI.GetComponent<ProgressBarUI>().slider.value = ((float)KeyPressCount / targetKeyPresses);

        HandleMusicSFX();
    }

    private void HandleMusicSFX()
    {
        if (GetComponent<SFX>().secondEffect == null)
        {
            GetComponent<SFX>().PlayFirstEffect();
            return;
        }

        int randomMusic = Random.Range(0, 2);
        if (randomMusic == 0)
        {
            GetComponent<SFX>().PlaySecondEffect();
        }
        else
        {
            GetComponent<SFX>().PlayFirstEffect();
        }
    }

    public void IncreasePlayerPoints(GameObject player)
    {
        SetObjectStateServerRpc(true);
        Debug.Log("R pressed 5 times AND POINTS ARE GAINED!");


        player.GetComponent<PlayerGainPoints>().GainPoints(howMuchPointGiveThisObject);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetObjectStateServerRpc(bool setVariable)
    {
        isExhausted.Value = setVariable;
        SetObjectStateClientRpc(setVariable);
    }

    [ClientRpc]
    private void SetObjectStateClientRpc(bool setVariable)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        if (setVariable)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = disableItemMaterial;
            vfx.GetComponent<ParticleSystem>().Stop();
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = enabledItemMaterial;
            vfx.GetComponent<ParticleSystem>().Play();
        }
    }
}
