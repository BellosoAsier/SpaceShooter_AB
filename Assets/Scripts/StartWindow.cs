using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour
{
    [SerializeField] private List<GameObject> listSpaceships;
    [SerializeField] private List<Image> listImages;

    [SerializeField] private List<GameObject> listGOToActivate;

    public int index = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject go in listGOToActivate)
            {
                go.SetActive(true);
            }
            gameObject.SetActive(false);
        }

        int newIndex = index-1;
        foreach (Image image in listImages)
        {
            if (newIndex == -1)
            {
                image.sprite = listSpaceships[listSpaceships.Count - 1].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
            else if (newIndex == listSpaceships.Count)
            {
                image.sprite = listSpaceships[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                image.sprite = listSpaceships[newIndex].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }

            transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = listSpaceships[index].GetComponent<PlayerBehaviour>().GetSpaceShipInformation().nameShip;
            newIndex++;
        }
    }

    public void MoveRight()
    {
        if (index == listSpaceships.Count-1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
    }

    public void MoveLeft()
    {
        if (index == 0)
        {
            index = listSpaceships.Count-1;
        }
        else
        {
            index--;
        }
    }
}
