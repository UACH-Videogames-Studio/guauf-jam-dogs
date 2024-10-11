using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line1 : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la línea de autos

    void Start()
    {
        // Asignar la velocidad a todos los hijos que tengan el script MoveCycle
        foreach (Transform child in transform)
        {
            MoveCycle moveCycle = child.GetComponent<MoveCycle>();
            if (moveCycle != null)
            {
                moveCycle.SetSpeed(speed); // Establece la velocidad del auto
            }
        }
    }

    void Update()
    {
        UpdateChildSpeeds(); 
    }

    private void UpdateChildSpeeds()
    {
        // Asignar la velocidad a todos los hijos que tengan el script MoveCycle
        foreach (Transform child in transform)
        {
            MoveCycle moveCycle = child.GetComponent<MoveCycle>();
            if (moveCycle != null)
            {
                moveCycle.SetSpeed(speed); // Establece la velocidad del auto
            }
        }
    }
}

