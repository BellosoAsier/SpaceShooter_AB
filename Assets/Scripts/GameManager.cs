using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listSpaceShips;
    [SerializeField] private GameObject waveIndUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject statisticsUI;
    [SerializeField] private List<List<int>> listDifficulty;

    private bool isPaused = false;
    private bool isStatisticScreen = false;
    public static GameObject spaceship;
    public static float timer;

    private void Start()
    {
        timer = 0f;
        int code = PlayerPrefs.GetInt("ShipCode", 0);
        spaceship = Instantiate(listSpaceShips[code], new Vector3(-8f ,0f ,0f), Quaternion.identity);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isStatisticScreen)
        {
            StatisticReturnGame();
        }
        else
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Detiene el tiempo
        isPaused = true;
        waveIndUI.SetActive(false);
        menuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        isPaused = false;
        waveIndUI.SetActive(true);
        menuUI.SetActive(false);
    }

    public void StatisticGame()
    {
        isStatisticScreen = true;
        statisticsUI.SetActive(true);
        waveIndUI.SetActive(false);
        menuUI.SetActive(false);
    }

    public void StatisticReturnGame()
    {
        isStatisticScreen = false;
        statisticsUI.SetActive(false);
        waveIndUI.SetActive(true);
        menuUI.SetActive(true);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("00_InitiaScene");
    }
}
