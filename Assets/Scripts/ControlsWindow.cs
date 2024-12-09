using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsWindow : MonoBehaviour
{
    [SerializeField] private List<GameObject> listToActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeVisual();
        }
    }

    public void ChangeVisual()
    {
        foreach (GameObject item in listToActivate)
        {
            item.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
