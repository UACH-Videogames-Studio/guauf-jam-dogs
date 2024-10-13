using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SlowDownLineSkill : MonoBehaviour
{
    public float slowSpeed = 2f;
    public float slowDuration = 5f; 

    void Update()
    {
        // Detectar si se presiona la tecla "R" para ralentizar la línea 1
        if (Input.GetKeyDown(KeyCode.R))
        {
            VehicleEvents.TriggerSlowDown("line1", slowSpeed, slowDuration); // Ralentizar la línea 1
        }

        // Detectar si se presiona la tecla "T" para ralentizar la línea 2
        if (Input.GetKeyDown(KeyCode.T))
        {
            VehicleEvents.TriggerSlowDown("line2", slowSpeed, slowDuration); // Ralentizar la línea 2
        }
    }
}
