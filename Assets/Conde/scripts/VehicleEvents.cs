using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class VehicleEvents
{
    // Evento que incluye una línea específica, velocidad y duración
    public static event Action<string, float, float> OnSlowDown;

    // Método para disparar el evento de ralentización
    public static void TriggerSlowDown(string lineIdentifier, float speed, float duration)
    {
        if (OnSlowDown != null)
        {
            OnSlowDown(lineIdentifier, speed, duration); // Disparador
        }
    }
}
