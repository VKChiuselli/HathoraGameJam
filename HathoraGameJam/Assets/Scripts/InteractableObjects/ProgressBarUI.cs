using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProgressBarUI : MonoBehaviour
{


    [SerializeField] public Image barImage;
    [SerializeField] public  Slider slider;
    [SerializeField] public  TextMeshProUGUI tooltipText;
    public bool isOnline;

    private IHasProgress hasProgress;


    private void Start()
    {
   

 
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }



}