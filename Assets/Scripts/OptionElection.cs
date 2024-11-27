using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionElection : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text tmp;
    [SerializeField] private Color color;
    [SerializeField] private List<GameObject> listGOToDesactivate;
    [SerializeField] private List<GameObject> listGOToActivate;

    void Start()
    {
        tmp = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        tmp.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmp.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.name.Equals("Start"))
        {
            foreach(GameObject go in listGOToDesactivate)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in listGOToActivate)
            {
                go.SetActive(true);
            }
            tmp.color = Color.white;
        }
        else if (gameObject.name.Equals("Play"))
        {
            int ind = transform.parent.GetComponent<StartWindow>().index;
            PlayerPrefs.SetInt("ShipCode", ind); // Guardo un dato persistente "ShipCode" en el Registro de Windows.
            SceneManager.LoadSceneAsync("01_GameScene");
        }
        else if (gameObject.name.Equals("Exit"))
        {
            Application.Quit();
        }
    }
}
