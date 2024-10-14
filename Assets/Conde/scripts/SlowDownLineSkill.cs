using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class SlowDownLineSkill : MonoBehaviour
{
    public float slowSpeed = 2f;
    static public float slowDuration = 5f;

    [SerializeField] GameObject slowDownTrigger;

    void Update()
    {
        // Detectar si se presiona la tecla "R" para ralentizar la línea 1
        if (Input.GetKeyDown(KeyCode.X))
        {
            AcivateSlowDownTrigger();
        }
    }

    public void MoveSlowDownTrigger()
    {
        Vector3 position = slowDownTrigger.transform.position;
        position.y -= 2;
        slowDownTrigger.transform.position = position;
    }

    void AcivateSlowDownTrigger()
    {
        slowDownTrigger.SetActive(true);
        StartCoroutine(DeactivateSlowDownTrigger());
    }

    IEnumerator DeactivateSlowDownTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        if (!slowDownTrigger.activeInHierarchy)
        {
            slowDownTrigger.SetActive(false);
        }
    }
}

