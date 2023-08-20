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


    void Start()
    {
        TitleScenePrefabPlayButton.onClick.AddListener(GoToMainMenuScenePrefab);
        TitleScenePrefab.SetActive(true);
        MainMenuScenePrefab.SetActive(false);
    }

    private void GoToMainMenuScenePrefab()
    {
        MainMenuScenePrefab.SetActive(true);
        TitleScenePrefab.SetActive(false);
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
    }
}
