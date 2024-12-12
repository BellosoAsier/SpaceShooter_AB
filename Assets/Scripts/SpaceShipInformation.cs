using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShip", menuName = "ScriptableObjects/CreateSpaceShipSO", order = 1)]
public class SpaceShipInformation : ScriptableObject
{
    [SerializeField] public string nameShip;
    [SerializeField] public int health;
    [SerializeField] public float velocity;
    [SerializeField] public int shields;
    [SerializeField] public float armor;
    [SerializeField] public float attack;
    [SerializeField] public float attSpeed;
    [SerializeField] public int kills;
    [SerializeField] public int money;
}
