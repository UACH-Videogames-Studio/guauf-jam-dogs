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

    private bool allowSkill = true;

    private int remainingUses = 3;

    public AudioSource barkSound;
    void Update()
    {
        if (allowSkill && remainingUses > 0)
        {
            // Detectar si se presiona la tecla "R" para ralentizar la línea 1
            if (Input.GetKeyDown(KeyCode.Space))
            {
                barkSound.Play();
                AcivateSlowDownTrigger();
                remainingUses--;
            }
        }
    }
    public void Deactivate()
    {
        allowSkill = false;
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

    public void ResetUses()
    {
        remainingUses = 3;
    }

    public void SetUses(int newUses)
    {
        remainingUses = newUses;
    }
}

