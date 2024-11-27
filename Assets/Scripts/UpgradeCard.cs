using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity { Common, Rare, Epic, Legendary}
public enum Statistic { Health, Attack, Shotrate, Shields, Velocity, Armor }

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CreateCardSO", order = 3)]
public class UpgradeCard : ScriptableObject
{
    [Header("Card rarity")]
    [SerializeField] public Rarity rarity;
    //[SerializeField] private Sprite card;

    [Header("Price")]
    [SerializeField] public int costPrice;

    [Header("Upgrade")]
    [SerializeField] public Statistic statisticU;
    [SerializeField] public float upgradeValue;
    //[SerializeField] private Sprite upgradeSprite;

    [Header("Downgrade")]
    [SerializeField] public Statistic statisticD;
    [SerializeField] public float downgradeValue;
    //[SerializeField] private Sprite downgradeSprite;
}
