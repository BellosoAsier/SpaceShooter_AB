using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listSpaceShips;
    [SerializeField] private GameObject waveIndUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject statisticsUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private List<List<int>> listDifficulty;

    [Header("Background")]
    [SerializeField] private GameObject background;

    [Serializable]
    public class WorldSprites
    {
        public string name;
        public Sprite back;
        public Sprite part1;
        public Sprite part2;
    }
    [SerializeField] private List<WorldSprites> listWorldSprites;

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
        SceneManager.LoadSceneAsync("00_InitialScene");
    }

    public void ToggleShop()
    {
        waveIndUI.SetActive(false);
        shopUI.SetActive(true);
        shopUI.GetComponent<Shop>().enabled = true;
    }

    public void ChangeWorldSprites(int index)
    {
        background.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listWorldSprites[index].back;
        background.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = listWorldSprites[index].back;

        background.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = listWorldSprites[index].part1;
        background.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = listWorldSprites[index].part1;

        background.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = listWorldSprites[index].part2;
        background.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = listWorldSprites[index].part2;
    }
}
