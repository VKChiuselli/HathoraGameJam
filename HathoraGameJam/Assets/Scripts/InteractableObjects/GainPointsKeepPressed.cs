using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class GainPointsKeepPressed : NetworkBehaviour, IHasProgress
{
    private bool qKeyHeld = false;
 
    public float holdDurationRequired = 1.0f;  
    ScoreboardManager scoreBoard;
    public int howMuchPointGiveThisObject;
    NetworkVariable< bool> isExhausted = new NetworkVariable<bool>();
    NetworkVariable< float> holdStartTimeNetwork = new NetworkVariable<float>();
    float holdStartTime;
    [SerializeField] GameObject progressBarUI;
    [SerializeField] GameObject vfx;
  public  Material enabledItemMaterial;
  public  Material disableItemMaterial;

    public ulong currentPlayerId;

    private void Start()
    {
        currentPlayerId = NetworkManager.LocalClient.ClientId;
        progressBarUI.GetComponent<ProgressBarUI>().tooltipText.text = "Hold Q";
        isExhausted.Value = false;
        if (howMuchPointGiveThisObject == 0)
        {
        howMuchPointGiveThisObject = 20;
        }
        scoreBoard = GameObject.Find("ScoreBoardManagerCanvas").GetComponent<ScoreboardManager>();
    }


    private void OnTriggerStay(Collider other)
    {

        if (!oneTime)
        {
            if ((other.tag == "Player" || other.tag == "Immortal") &&  other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
            {
            //    PppoServerRpc();
                if (!isExhausted.Value)
                {
                    SetProgressBarUI(holdStartTime, true);
                    if (Input.GetKey(KeyCode.Q))
                    {
                        if (!qKeyHeld)
                        {
                            qKeyHeld = true;
                            holdStartTime = Time.time;
                            //            SetProgressBarUI(holdStartTime , true );
                        }

                        if (qKeyHeld && Time.time - holdStartTime >= holdDurationRequired)
                        {
                            oneTime = true;
                            SetProgressBarUI(holdStartTime, false);
                            IncreasePlayerPoints(other);
                            //  StartCoroutine(IncreasePlayerPoints(other));
                        }
                    }
                    else
                    {
                        holdStartTime = 0;//reset the counter UI
                   //     SetProgressBarUI(holdStartTime, false);
                        qKeyHeld = false;
                    }
                }
            }
        }


    }
   

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Immortal") && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            if (this.gameObject.GetComponent<MeshRenderer>().material == disableItemMaterial)
            {
                return;
            }
                progressBarUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Immortal") && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            progressBarUI.GetComponent<ProgressBarUI>().slider.value = 0;
            progressBarUI.SetActive(false);
            if (GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Stop();
            }
        }
    }




    private void SetProgressBarUI(float holdStartTime, bool state)
    {
        if (!state)
        {
            progressBarUI.GetComponent<ProgressBarUI>().isOnline = false;
            progressBarUI.SetActive(state);
            return;
        }
        else
        {
            progressBarUI.SetActive(state);
             Debug.Log("holdStartTime  " + holdStartTime);
    //       
            progressBarUI.GetComponent<ProgressBarUI>().isOnline = true; 
            //TODO change value on the bar
        }
         
    }

    private float timer = 0f;
    private float interval = 1f; // 10 seconds interval

    private void Update()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            if (progressBarUI.GetComponent<ProgressBarUI>().isOnline)
            {

                timer += Time.deltaTime;
                GetComponent<SFX>().PlayFirstEffectLoop();
               
       
                Debug.Log("  progressBarUI.GetComponent<ProgressBarUI>().barImage.fillAmount  " + progressBarUI.GetComponent<ProgressBarUI>().barImage.fillAmount);
                    progressBarUI.GetComponent<ProgressBarUI>().slider.value = timer / holdDurationRequired;
 
                }

        }
        else
        {
            timer = 0;
            progressBarUI.GetComponent<ProgressBarUI>().slider.value = 0;
        }
    }



          

     
 

    public float resetTime = 5f;
    private bool oneTime;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public override void OnNetworkSpawn()
    {
        isExhausted.OnValueChanged += isExhausted_OnValueChanged;
    
    }

   

    private void isExhausted_OnValueChanged(bool previousValue, bool newValue)
    {
        Debug.Log("isExhausted_OnValueChanged changed! keep pressed");
        if (newValue)
        {
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = 1f
            });
        }
    }

    public void IncreasePlayerPoints(Collider other)
    {
        SetObjectStateServerRpc(true);
        Debug.Log("Q key held for 1 second AND POINTS ARE GAINED!");

        other.gameObject.GetComponent<PlayerGainPoints>().GainPoints(howMuchPointGiveThisObject);
 

    }

    [ServerRpc(RequireOwnership =false)]
    private void SetObjectStateServerRpc(bool setVariable)
    {
        isExhausted.Value = setVariable;
        SetObjectStateClientRpc(setVariable);
    }

    [ClientRpc]
    private void SetObjectStateClientRpc(bool setVariable)
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
        GetComponent<AudioSource>().Stop();
        }
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
