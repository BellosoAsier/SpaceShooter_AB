using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private float delaySecods;
    [SerializeField] private int numberOfLevels;
    [SerializeField] private int numberOfWaves;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private List<GameObject> enemiesGO;
    [SerializeField] private TextMeshProUGUI textWaves;
    [SerializeField] private GameObject boss;

    private int indexWaves=1;
    private int indexLevel=1;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnEnemies(5f, 4f, 2f, numberOfLevels, numberOfWaves, numberOfEnemies));
        //StartCoroutine(SpawnEnemiesPerWave(2f, 5f));
    }

    private void OnEnable()
    {
       StartCoroutine(SpawnEnemiesPerWave(2f,5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemiesPerWave(float secEnemies, float secWave)
    {
        if (indexLevel == numberOfLevels && numberOfWaves==indexWaves)
        {
            textWaves.text = "Boss - Dead Star";
            boss.SetActive(true);
            boss.GetComponent<BossBehaviour>().enabled = true;
        }
        else
        {
            textWaves.text = "Wave " + indexLevel + " - " + indexWaves;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector3 randomPoint = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), transform.position.z);
                Instantiate(enemiesGO[Random.Range(0, enemiesGO.Count)], randomPoint, Quaternion.identity);
                yield return new WaitForSeconds(secEnemies);
            }

            yield return new WaitForSeconds(secWave);
            if (indexWaves == numberOfWaves)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeWorldSprites(indexLevel);
                indexLevel++;
                indexWaves = 1;
            }
            else
            {
                indexWaves++;
            }
            GameManager.spaceship.GetComponent<PlayerBehaviour>().enabled = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().ToggleShop();
            GetComponent<EnemySpawnerBehaviour>().enabled = false;
        }
        
        
    }

    IEnumerator SpawnEnemies(float secLvl, float secWv, float secEn, int numberOfLevels, int numberOfWaves, int numberOfEnemies)
    {
        for (int i = 0; i < numberOfLevels; i++)
        {
            for (int j = 0; j < numberOfWaves; j++)
            {
                textWaves.text = "Wave " + (i+1) + " - " + (j + 1);
                for (int k = 0; k < numberOfEnemies; k++)
                {
                    Vector3 randomPoint = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), transform.position.z);
                    Instantiate(enemiesGO[Random.Range(0, enemiesGO.Count)], randomPoint, Quaternion.identity);
                    yield return new WaitForSeconds(secEn);
                }
                yield return new WaitForSeconds(secWv);
            }
            yield return new WaitForSeconds(secLvl);
        }
        
    }
}
