using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    private float maxhealth;
    private string fullText = "You Win...";
    private string currentText = "";
    [SerializeField] public float health;
    [SerializeField] private float damage;
    private GameObject attack1;
    private GameObject attack2;
    [SerializeField] private AudioClip laserBeamSound;
    [SerializeField] private AudioClip attackIndicatorSound;
    [SerializeField] private Image healthContainer;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private bool onlyOnce = true;
    private Coroutine attackCoroutine;
    private void OnEnable()
    {
        maxhealth = health;
        attack1 = transform.GetChild(0).gameObject;
        attack2 = transform.GetChild(1).gameObject;
        attackCoroutine = StartCoroutine(BossAttack());
    }

    private void Update()
    {
        healthContainer.fillAmount = health / maxhealth;

        if (health <=0 && onlyOnce)
        {
            StopCoroutine(attackCoroutine);
            StartCoroutine(TypeText());
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

    IEnumerator TypeText()
    {
        onlyOnce = false;
        textMeshPro.gameObject.SetActive(true);
        foreach (char letter in fullText)
        {
            currentText += letter; // Añadir una letra al texto
            textMeshPro.text = currentText; // Mostrar el texto en la UI
            yield return new WaitForSeconds(0.3f); // Esperar un poco antes de agregar la siguiente letra
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("00_InitialScene");
    }

    public float GetDamage()
    {
        return damage;
    }
}
