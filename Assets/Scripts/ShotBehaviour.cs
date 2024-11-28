using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float velocity;
    private float damage;

    private ObjectPool<ShotBehaviour> myPool;

    public ObjectPool<ShotBehaviour> MyPool { get => myPool; set => myPool = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    public void SetDamage(float x)
    {
        damage = x;
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Limit"))
        {
            myPool.Release(this);
        }
    }

    public void PlayerShotChanges()
    {
        gameObject.tag = "PlayerShot";
        GetComponent<SpriteRenderer>().color = Color.green;
        direction = new Vector3(1f, 0f, 0f);
        velocity = 10;
    }

    public void EnemyShotChanges()
    {
        gameObject.tag = "EnemyShot";
        GetComponent<SpriteRenderer>().color = Color.red;
        direction = new Vector3(-1f, 0f, 0f);
        velocity = 10;
    }

}
