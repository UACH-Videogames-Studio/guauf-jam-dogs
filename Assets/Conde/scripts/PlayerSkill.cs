using System.Collections;
using UnityEngine;

// ===
// @deprecated
// ===
public class PlayerSkill : MonoBehaviour
{
    public float slowSpeed = 2f;
    static public float slowDuration = 5f;
    private DogInputActions dogInputActions;

    [SerializeField] GameObject slowDownCollider;
    private void Awake() //Esto es necesario para que jale el input system
    {
        dogInputActions = new DogInputActions();
    }
    private void OnEnable() //Esto es necesario para que jale el input system
    {
        dogInputActions.Enable();
    }
    private void OnDisable() //Esto es necesario para que jale el input system
    {
        dogInputActions.Disable();
    }
    void Update()
    {
        // Detectar si se presiona la tecla "R" para ralentizar la línea 1
        if(dogInputActions.Base.Bark.WasPressedThisFrame()) //if(Input.GetKeyDown(KeyCode.Space)) <- Este es el antiguo
        {
            AcivateSlowDownCollider();
            // VehicleEvents.TriggerSlowDown("line1", slowSpeed, slowDuration); // Ralentizar la línea 1
        }
    }

    public void MoveSlowDownCollider()
    {
        Vector3 position = slowDownCollider.transform.position;
        position.y -= 2;
        slowDownCollider.transform.position = position;
    }

    void AcivateSlowDownCollider()
    {
        slowDownCollider.SetActive(true);
        StartCoroutine(DeactivateSlowDownCollider());
    }

    IEnumerator DeactivateSlowDownCollider()
    {
        yield return new WaitForSeconds(0.5f);
        if (!slowDownCollider.activeInHierarchy)
        {
            slowDownCollider.SetActive(false);
        }
    }
}

