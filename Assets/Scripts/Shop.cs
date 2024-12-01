using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<GameObject> slots;
    [SerializeField] private List<GameObject> slotsPrices;
    [SerializeField] private List<GameObject> slotsButtons;
    [SerializeField] private List<UpgradeCard> cards;

    [SerializeField] private List<Sprite> listSprites;

    [SerializeField] private GameObject waveUI;
    private System.Random rnd = new System.Random();
    private List<UpgradeCard> finalList = null;
    // Start is called before the first frame update
    void Start()
    {
        //ChooseCards();
    }

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            slotsButtons[i].GetComponent<Button>().interactable = true;
        }
        ChooseCards();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (finalList[i].costPrice > GameManager.spaceship.GetComponent<PlayerBehaviour>().money)
            {
                slotsButtons[i].GetComponent<Button>().interactable = false;
            }
        }
        transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.spaceship.GetComponent<PlayerBehaviour>().money.ToString();
    }

    private void ChooseCards()
    {
        finalList = cards.OrderBy(x => rnd.Next()).Take(slots.Count).ToList();

        for (int i = 0; i < slots.Count; i++)
        {
            foreach (Sprite sprite in listSprites)
            {
                if (sprite.name.StartsWith(finalList[i].rarity.ToString()))
                {
                    slots[i].GetComponent<Image>().sprite = sprite;
                }
            }

            slots[i].transform.GetChild(0).GetComponent<TMP_Text>().text = finalList[i].upgradeValue.ToString() + " "+ finalList[i].statisticU.ToString();
            foreach (Sprite sprite in listSprites)
            {
                if (sprite.name.StartsWith(finalList[i].statisticU.ToString()))
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite;
                }
            }

            slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = finalList[i].downgradeValue.ToString() + " " + finalList[i].statisticD.ToString();
            foreach (Sprite sprite in listSprites)
            {
                if (sprite.name.StartsWith(finalList[i].statisticD.ToString()))
                {
                    slots[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = sprite;
                }
            }

            slotsPrices[i].GetComponent<TMP_Text>().text = finalList[i].costPrice.ToString();
        }
    }

    public void BuyCard(int x)
    {
        if(GameManager.spaceship.GetComponent<PlayerBehaviour>().money >= finalList[x].costPrice)
        {
            GameManager.spaceship.GetComponent<PlayerBehaviour>().money -= finalList[x].costPrice;

            ChangeStatistics(finalList[x].statisticU, x, true);

            ChangeStatistics(finalList[x].statisticD, x, false);

            slotsButtons[x].GetComponent<Button>().interactable = false;
        }
    }

    public void NextButton()
    {
        GetComponent<Shop>().enabled = false;
        gameObject.SetActive(false);
        waveUI.SetActive(true);
        GameManager.spaceship.GetComponent<PlayerBehaviour>().enabled = true;
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().enabled = true;
    }

    private void ChangeStatistics(Statistic y, int x, bool isUpgrade)
    {
        switch (y)
        {
            case Statistic.Health:
                if (isUpgrade)
                {
                    GameManager.spaceship.GetComponent<PlayerBehaviour>().healthValue += (int)finalList[x].upgradeValue;
                }
                else
                {
                    if ((GameManager.spaceship.GetComponent<PlayerBehaviour>().healthValue + (int)finalList[x].downgradeValue)<= 0)
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().healthValue = 1;
                    }
                    else
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().healthValue += (int)finalList[x].downgradeValue;
                    }
                }
                break;
            case Statistic.Armor:
                if (isUpgrade)
                {
                    GameManager.spaceship.GetComponent<PlayerBehaviour>().armor += finalList[x].upgradeValue;
                }
                else
                {
                    if ((GameManager.spaceship.GetComponent<PlayerBehaviour>().armor + finalList[x].downgradeValue) <= 0)
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().armor = 1;
                    }
                    else
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().armor += finalList[x].downgradeValue;
                    }
                }
                break;
            case Statistic.Attack:
                if (isUpgrade)
                {
                    GameManager.spaceship.GetComponent<PlayerBehaviour>().attack += finalList[x].upgradeValue;
                }
                else
                {
                    if ((GameManager.spaceship.GetComponent<PlayerBehaviour>().attack + finalList[x].downgradeValue) <= 1)
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().attack = 1f;
                    }
                    else
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().attack += finalList[x].downgradeValue;
                    }
                }
                break;
            case Statistic.Shields:
                if (isUpgrade)
                {
                    GameManager.spaceship.GetComponent<PlayerBehaviour>().shields += (int)finalList[x].upgradeValue;
                }
                else
                {
                    if ((GameManager.spaceship.GetComponent<PlayerBehaviour>().shields + finalList[x].downgradeValue) <= 0)
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().shields = 0;
                    }
                    else
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().shields += (int) finalList[x].downgradeValue;
                    }
                }
                break;
            case Statistic.Shotrate:
                if (isUpgrade)
                {
                    if ((GameManager.spaceship.GetComponent<PlayerBehaviour>().shotRate + finalList[x].downgradeValue) <= 0.1f)
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().shotRate = 0.1f;
                    }
                    else
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().shotRate += finalList[x].downgradeValue;
                    }
                }
                else
                {
                    GameManager.spaceship.GetComponent<PlayerBehaviour>().shotRate += finalList[x].downgradeValue;
                }
                break;
            case Statistic.Velocity:
                if (isUpgrade)
                {
                    GameManager.spaceship.GetComponent<PlayerBehaviour>().velocity += finalList[x].upgradeValue;
                }
                else
                {
                    if ((GameManager.spaceship.GetComponent<PlayerBehaviour>().velocity + finalList[x].downgradeValue) <= 1)
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().velocity = 1;
                    }
                    else
                    {
                        GameManager.spaceship.GetComponent<PlayerBehaviour>().velocity += finalList[x].downgradeValue;
                    }
                }
                break;

            case Statistic.Enemies:
                if (isUpgrade)
                {
                    GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().SetNumberOfEnemies((int)(GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().GetNumberOfEnemies() + finalList[x].upgradeValue));
                }
                else
                {
                    if ((GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().GetNumberOfEnemies() + finalList[x].downgradeValue) <= 1)
                    {
                        GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().SetNumberOfEnemies(1);
                    }
                    else
                    {
                        GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().SetNumberOfEnemies((int)(GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().GetNumberOfEnemies() + finalList[x].downgradeValue));
                    }
                }
                break;
        }
    }
}
