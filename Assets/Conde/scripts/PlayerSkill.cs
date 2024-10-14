using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerSkill : MonoBehaviour
{
    public float slowSpeed = 2f;
    static public float slowDuration = 5f;
    
    [SerializeField] GameObject slowDownCollider;

    void Update()
    {
        // Detectar si se presiona la tecla "R" para ralentizar la línea 1
        if (Input.GetKeyDown(KeyCode.X))
        {
            AcivateSlowDownCollider();
            // VehicleEvents.TriggerSlowDown("line1", slowSpeed, slowDuration); // Ralentizar la línea 1
        }
    }

    public void MoveSlowDownCollider(){
        Vector3 position=slowDownCollider.transform.position;
        position.y-=2;
        slowDownCollider.transform.position=position;
    }

    void AcivateSlowDownCollider(){
        slowDownCollider.SetActive(true);
        StartCoroutine(DeactivateSlowDownCollider());
    }
    
    IEnumerator DeactivateSlowDownCollider(){
        yield return new WaitForSeconds(0.5f);
        if(!slowDownCollider.activeInHierarchy){
            slowDownCollider.SetActive(false);
        }
    }
}

