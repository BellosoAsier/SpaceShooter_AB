using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceParallax : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float widthImage;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float space = velocity * Time.time; //Espacio que recorre en un tiempo
        float rest = space % widthImage; // Cuanto me queda por recorrer para alcanzar un nuevo ciclo

        transform.position = initialPosition + rest * direction; 

    }
}
