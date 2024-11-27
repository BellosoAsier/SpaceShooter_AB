using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static Unity.Burst.Intrinsics.X86;

public class EnemyBehaviour : MonoBehaviour
{
    private float healthValue;
    private float velocity;
    private float armor;
    private float attack;
    private float shotRate;
    private int moneyReward;

    [SerializeField] private EnemyInformation enemyInfo;
    [SerializeField] private Vector3 direction;
    [SerializeField] private ShotBehaviour enemyShot;
    [SerializeField] private GameObject explosion;

    private ObjectPool<ShotBehaviour> pool;

    private void Awake()
    {
        pool = new ObjectPool<ShotBehaviour>(CreateShot, null, ReleaseShot, DestroyShot);
    }
    private void DestroyShot(ShotBehaviour behaviour)
    {
        Destroy(behaviour);
    }

    private void ReleaseShot(ShotBehaviour behaviour)
    {
        behaviour.gameObject.SetActive(false);
    }

    private void GetShot(ShotBehaviour behaviour)
    {

    }

    private ShotBehaviour CreateShot()
    {
        ShotBehaviour shot = Instantiate(enemyShot, transform.position, Quaternion.identity);
        shot.MyPool = pool;
        return shot;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = enemyInfo.nameShip;
        healthValue = enemyInfo.health;
        velocity = enemyInfo.velocity;
        shotRate = enemyInfo.attSpeed;
        armor = enemyInfo.armor;
        attack = enemyInfo.attack;
        moneyReward = enemyInfo.moneyReward;

        StartCoroutine(SpawnEnemyShots(shotRate));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    IEnumerator SpawnEnemyShots(float delaySeconds)
    {
        for (int i = 0; i<1; i++)
        {
            GetComponent<AudioSource>().Play();
            ShotBehaviour x = pool.Get();
            x.transform.position = transform.position;
            x.SetDamage(attack);
            x.gameObject.SetActive(true);
            yield return new WaitForSeconds(delaySeconds);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerShot"))
        {
            healthValue -= other.gameObject.GetComponent<ShotBehaviour>().GetDamage() / armor;
            other.GetComponent<ShotBehaviour>().MyPool.Release(other.gameObject.GetComponent<ShotBehaviour>());
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            healthValue -= other.gameObject.transform.parent.GetComponent<PlayerBehaviour>().GetDamage() / armor;
        }

        if (other.gameObject.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }

        if (healthValue < 0)
        {
            GameManager.spaceship.GetComponent<PlayerBehaviour>().kills++;
            GameManager.spaceship.GetComponent<PlayerBehaviour>().money += this.GetMoneyReward();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public float GetDamage()
    {
        return attack;
    }

    public int GetMoneyReward()
    {
        return moneyReward;
    }
}
