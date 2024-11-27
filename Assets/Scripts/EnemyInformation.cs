using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/CreateEnemySO", order = 2)]
public class EnemyInformation : ScriptableObject
{
    [SerializeField] public string nameShip;
    [SerializeField] public int health;
    [SerializeField] public float velocity;
    [SerializeField] public float armor;
    [SerializeField] public float attack;
    [SerializeField] public float attSpeed;
    [SerializeField] public int moneyReward;
}
