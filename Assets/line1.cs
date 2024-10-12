using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line1 : MonoBehaviour
{
    public float normalSpeed = 5f;   // Velocidad normal de la línea de autos
    public float slowSpeed = 2f;     // Velocidad ralentizada de la línea de autos
    private float speed;      // Velocidad actual de la línea
    private bool isSlowed = false;   // Saber si la línea ya está ralentizada
    public float slowDuration = 5f;  // Duración de la ralentización

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSlowed)
        {
            SlowDownLine();
        }
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

        public void SlowDownLine()
    {
        if (!isSlowed)  // Solo aplicar si no está ralentizada
        {
            isSlowed = true;
            speed = slowSpeed;  // Cambiar a la velocidad ralentizada
            UpdateChildSpeeds();       // Actualizar la velocidad de todos los hijos
            StartCoroutine(RestoreSpeedAfterDelay());  // Restaurar la velocidad después del tiempo
        }
    }

    // Corutina para restaurar la velocidad después de que pase la duración de ralentización
    private IEnumerator RestoreSpeedAfterDelay()
    {
        yield return new WaitForSeconds(slowDuration);
        speed = normalSpeed;  // Restaurar la velocidad normal
        isSlowed = false;            // Marcar que ya no está ralentizada
        UpdateChildSpeeds();         // Actualizar la velocidad de los hijos nuevamente
    }
}

