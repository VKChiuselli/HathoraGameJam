using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ScenePrefabManager : MonoBehaviour
{

    [SerializeField] GameObject BossOne;
    [SerializeField] GameObject BossTwo;
    [SerializeField] GameObject TitleScenePrefab;
    [SerializeField] Button TitleScenePrefabPlayButton;
    [SerializeField] GameObject MainMenuScenePrefab;
    [SerializeField] GameObject TimerUICanvas;
    [SerializeField] GameObject spawnWeaponPowerUpManager;
    [SerializeField] GameObject CameraStuff;
    public GameObject MainCameraTitleScene;
    [SerializeField]  GameObject MainCameraGame;
    [SerializeField]  GameObject AudioSourceManager;


    void Start()
    {
        TitleScenePrefabPlayButton.onClick.AddListener(GoToMainMenuScenePrefab);
        TitleScenePrefab.SetActive(true);
        MainMenuScenePrefab.SetActive(false);
        AudioSourceManager.GetComponent<SFX>().PlayFirstEffect();
    }

    private void GoToMainMenuScenePrefab()
    {
        MainMenuScenePrefab.SetActive(true);
        TitleScenePrefab.SetActive(false);
        MainCameraGame.SetActive(true);
        MainCameraTitleScene.SetActive(false);
    }

    public void StartGame()
    {
        spawnWeaponPowerUpManager.GetComponent<SpawnWeaponPowerUpManager>().IsGameStarted = true;
        BossOne.GetComponent<NavMeshAgent>().speed = 2.5f;
        BossTwo.GetComponent<NavMeshAgent>().speed = 2;
        MainMenuScenePrefab.SetActive(false);
        TitleScenePrefab.SetActive(false);
        TimerUICanvas.GetComponent<TimerUI>().isGameStarted = true;
        CameraStuff.SetActive(true);
        MainCameraGame.SetActive(true);
        AudioSourceManager.GetComponent<SFX>().StopFirstEffect();
        AudioSourceManager.GetComponent<SFX>().PlaySecondEffect();
    }
}
