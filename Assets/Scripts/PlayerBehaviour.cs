using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.Video;

public class PlayerBehaviour : MonoBehaviour
{
    public float healthValue;
    public float velocity;
    public float shotRate;
    public int shields;
    public float armor;
    public float attack;
    public int kills = 0;
    public int money = 0;

    [SerializeField] private SpaceShipInformation ssi;
    [SerializeField] private ShotBehaviour shotPrefab;
    [SerializeField] private List<GameObject> listAttaches;
    [SerializeField] private GameObject explosion;
    private float timer;

    private ObjectPool<ShotBehaviour> pool;

    //[SerializeField] private GameObject spaceShipFire;
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
        ShotBehaviour shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        shot.MyPool = pool;
        return shot;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = ssi.nameShip;
        healthValue = ssi.health;
        velocity = ssi.velocity;
        shotRate = ssi.attSpeed;
        timer = ssi.attSpeed;
        shields = ssi.shields;
        armor = ssi.armor;
        attack = ssi.attack;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ClampMovement();
        PlayerShot();
    }

    private void PlayerMovement()
    {
        float newVelocity = velocity;
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        if (horizontalAxis > 0)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            newVelocity = velocity * 1.5f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            newVelocity = velocity;
        }
        transform.Translate(new Vector2(horizontalAxis, verticalAxis).normalized * newVelocity * Time.deltaTime);
    }

    private void ClampMovement()
    {
        float xClampped = Mathf.Clamp(transform.position.x, -8.27f, 8.27f);
        float yClampped = Mathf.Clamp(transform.position.y, -4.4f, 4.4f);
        transform.position = new Vector3(xClampped, yClampped, 0f);
    }

    private void PlayerShot()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && timer > shotRate)
        {
            GetComponent<AudioSource>().Play();
            foreach (GameObject go in listAttaches)
            {
                ShotBehaviour x = pool.Get();
                x.transform.position = go.transform.position;
                x.GetComponent<ShotBehaviour>().SetDamage(attack);
                x.gameObject.SetActive(true);
            }
            timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyShot"))
        {
            healthValue -= other.gameObject.GetComponent<ShotBehaviour>().GetDamage() / armor;
            Debug.Log(healthValue);
            other.GetComponent<ShotBehaviour>().MyPool.Release(other.gameObject.GetComponent<ShotBehaviour>());
            //Destroy(other.gameObject);
        }

        else if(other.gameObject.CompareTag("Enemy")) 
        {
            Debug.Log("hhh");
            healthValue -= other.transform.parent.gameObject.GetComponent<EnemyBehaviour>().GetDamage() / armor;
        }

        if (healthValue<0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public float GetDamage()
    {
        return attack;
    }

    public SpaceShipInformation GetSpaceShipInformation()
    {
        return ssi;
    }
}
