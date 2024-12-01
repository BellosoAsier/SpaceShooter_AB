using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    private float maxhealth;
    [SerializeField] public float health;
    [SerializeField] private float damage;
    private GameObject attack1;
    private GameObject attack2;
    [SerializeField] private AudioClip laserBeamSound;
    [SerializeField] private AudioClip attackIndicatorSound;
    [SerializeField] private Image healthContainer;

    private void OnEnable()
    {
        maxhealth = health;
        attack1 = transform.GetChild(0).gameObject;
        attack2 = transform.GetChild(1).gameObject;
        StartCoroutine(BossAttack());
    }

    private void Update()
    {
        healthContainer.fillAmount = health / maxhealth;

        if (health <=0)
        {
            Debug.Log("WIN");
            Destroy(gameObject);
        }
    }

    IEnumerator BossAttack()
    {
        while (true)
        {
            int posY1 = Random.Range(-3,3);
            int posY2;

            do
            {
                posY2 = Random.Range(-3, 3);
            } while (posY2 == posY1);

            attack1.transform.position = new Vector3(attack1.transform.position.x,posY1, attack1.transform.position.z);
            attack2.transform.position = new Vector3(attack2.transform.position.x, posY2, attack2.transform.position.z);

            GetComponent<AudioSource>().clip = attackIndicatorSound;
            GetComponent<AudioSource>().Play();

            attack1.transform.GetChild(0).gameObject.SetActive(true);
            attack2.transform.GetChild(0).gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = laserBeamSound;
            GetComponent<AudioSource>().Play();
            attack1.transform.GetChild(0).gameObject.SetActive(false);
            attack2.transform.GetChild(0).gameObject.SetActive(false);
            attack1.transform.GetChild(1).gameObject.SetActive(true);
            attack2.transform.GetChild(1).gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            GetComponent<AudioSource>().Stop();
            attack1.transform.GetChild(1).gameObject.SetActive(false);
            attack2.transform.GetChild(1).gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
