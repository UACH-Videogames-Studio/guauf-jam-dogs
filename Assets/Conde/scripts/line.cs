using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class line : MonoBehaviour
{
    public float normalSpeed = 5f;   // Velocidad normal de la línea de autos
    public float slowSpeed = 2f;     // Velocidad ralentizada de la línea de autos
    [SerializeField] private bool lineToRight = true;      // Velocidad actual de la línea
    private float speed = 5;      // Velocidad actual de la línea
    private bool isSlowed = false;   // Saber si la línea ya está ralentizada
    private Vector2 direction;

    void Start()
    {
        direction = lineToRight ? Vector2.right : Vector2.left;
        speed = normalSpeed;
        UpdateChildren();
    }

    // void OnEnable()
    // {
    //     // Asignar la velocidad a todos los hijos que tengan el script MoveCycle
    //     VehicleEvents.OnSlowDown += SlowDownLine;
    // }

    // void OnDisable()
    // {
    //     VehicleEvents.OnSlowDown -= SlowDownLine;
    // }

    void Update()
    {
        if(!isSlowed){
            speed=normalSpeed;
        }
        UpdateChildren();
    }

    private void UpdateChildren()
    {
        // Asignar la velocidad a todos los hijos que tengan el script MoveCycle
        foreach (Transform child in transform)
        {
            MoveCycle moveCycle = child.GetComponent<MoveCycle>();
            if (moveCycle != null)
            {
                moveCycle.SetDirection(direction);
                moveCycle.SetSpeed(speed); // Establece la velocidad del auto
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SlowDownTrigger"))
        {
            Debug.Log("DESACELERAR");
            other.gameObject.SetActive(false);
            SlowDownLine(SlowDownLineSkill.slowDuration);
        }
    }
    public void SlowDownLine(float duration)
    {
        if (!isSlowed)  // Solo aplicar si no está ralentizada
        {
            isSlowed = true;
            speed = slowSpeed;  // Cambiar a la velocidad ralentizada
            UpdateChildren();       // Actualizar la velocidad de todos los hijos
            StartCoroutine(RestoreSpeedAfterDelay(duration));  // Restaurar la velocidad después del tiempo
        }
    }

    // Corutina para restaurar la velocidad después de que pase la duración de ralentización
    private IEnumerator RestoreSpeedAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = normalSpeed;  // Restaurar la velocidad normal
        isSlowed = false;            // Marcar que ya no está ralentizada
        UpdateChildren();         // Actualizar la velocidad de los hijos nuevamente
    }
}

