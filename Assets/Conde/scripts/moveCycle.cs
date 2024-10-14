using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCycle : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // Dirección de movimiento
    public float speed = 1f; // Velocidad del auto
    // private float carWidth = 1f; // Tamaño del auto

    private Vector3 leftEdge; // Límite izquierdo
    private Vector3 rightEdge; // Límite derecho
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        // Obtiene los límites de la pantalla
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        // carWidth = spriteRenderer.bounds.size.x;
    }

    private void Update()
    {
        // Mueve el auto en la dirección especificada
        rb.velocity=direction * speed;

            Vector3 position = transform.position;

        // Verifica si el auto está completamente fuera de los límites derecho o izquierdo
        if (direction.x > 0 && transform.position.x > rightEdge.x)
        {
            // Reposiciona el auto al límite izquierdo
            position.x = leftEdge.x - 2; // Respawn al lado izquierdo
            transform.position = position;
        }
        else if (direction.x < 0 && transform.position.x < leftEdge.x)
        {
            // Reposiciona el auto al límite derecho
            position.x = rightEdge.x + 2; // Respawn al lado derecho
            transform.position = position;
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection; // Establece la velocidad
        if (direction.x >= 0)
        {
            // spriteRenderer.flipX=false;
            transform.localScale = new Vector2(
                Math.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
        else
        {
            transform.localScale = new Vector2(
                -Math.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // Establece la velocidad
    }
}
