using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticScreen : MonoBehaviour
{
    [SerializeField] private List<GameObject> listStatistic;

    private void Update()
    {
        listStatistic[0].GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().attack.ToString();
        listStatistic[1].GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().healthValue.ToString();
        listStatistic[2].GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().shields.ToString();
        listStatistic[3].GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().shotRate.ToString();
        listStatistic[4].GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().money.ToString();
        listStatistic[5].GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().kills.ToString();
        listStatistic[6].GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().velocity.ToString();

        int minutes = Mathf.FloorToInt(GameManager.timer / 60f); // Minutos
        int seconds = Mathf.FloorToInt(GameManager.timer % 60f); // Segundos restantes
        listStatistic[7].GetComponent<TMP_Text>().text = minutes + " min " + seconds + " seg";

        listStatistic[8].GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().armor.ToString();
        listStatistic[9].GetComponent<TMP_Text>().text = GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().GetNumberOfEnemies().ToString();
    }
}

