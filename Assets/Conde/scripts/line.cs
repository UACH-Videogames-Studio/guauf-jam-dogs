using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public string lineIdentifier = "line1";   // Identificador de la línea
    public float normalSpeed = 5f;   // Velocidad normal de la línea de autos
    public float slowSpeed = 2f;     // Velocidad ralentizada de la línea de autos
    private float speed;      // Velocidad actual de la línea
    private bool isSlowed = false;   // Saber si la línea ya está ralentizada

    void Start()
    {
        // Asignar la velocidad a todos los hijos que tengan el script MoveCycle
        foreach (Transform child in transform)
        {
            speed = normalSpeed;
            MoveCycle moveCycle = child.GetComponent<MoveCycle>();
            if (moveCycle != null)
            {
                moveCycle.SetSpeed(speed); // Establece la velocidad del auto
            }
        }
    }

    void OnEnable()
    {
        // Asignar la velocidad a todos los hijos que tengan el script MoveCycle
        VehicleEvents.OnSlowDown += SlowDownLine;
    }

    void OnDisable()
    {
        VehicleEvents.OnSlowDown -= SlowDownLine;
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

        public void SlowDownLine(string line, float newSpeed, float duration)
    {
        if (!isSlowed && line == lineIdentifier)  // Solo aplicar si no está ralentizada
        {
            isSlowed = true;
            speed = slowSpeed;  // Cambiar a la velocidad ralentizada
            UpdateChildSpeeds();       // Actualizar la velocidad de todos los hijos
            StartCoroutine(RestoreSpeedAfterDelay(duration));  // Restaurar la velocidad después del tiempo
        }
    }

    // Corutina para restaurar la velocidad después de que pase la duración de ralentización
    private IEnumerator RestoreSpeedAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = normalSpeed;  // Restaurar la velocidad normal
        isSlowed = false;            // Marcar que ya no está ralentizada
        UpdateChildSpeeds();         // Actualizar la velocidad de los hijos nuevamente
    }
}

