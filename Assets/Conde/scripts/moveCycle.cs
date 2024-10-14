using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCycle : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // Dirección de movimiento
    public float speed = 1f; // Velocidad del auto
    public float carWidth = 1f; // Tamaño del auto

    private Vector3 leftEdge; // Límite izquierdo
    private Vector3 rightEdge; // Límite derecho
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Obtiene los límites de la pantalla
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        spriteRenderer=GetComponent<SpriteRenderer>();
        carWidth = spriteRenderer.bounds.size.x;
    }

    private void Update()
    {
        // Mueve el auto en la dirección especificada
        transform.Translate(direction * speed * Time.deltaTime);

        // Verifica si el auto está completamente fuera de los límites derecho o izquierdo
        if (direction.x > 0 && transform.position.x > rightEdge.x)
        {
            // Reposiciona el auto al límite izquierdo
            Vector3 position = transform.position;
            position.x = leftEdge.x - carWidth; // Respawn al lado izquierdo
            transform.position = position;
        }
        else if (direction.x < 0 && (transform.position.x + carWidth) < leftEdge.x)
        {
            // Reposiciona el auto al límite derecho
            Vector3 position = transform.position;
            position.x = rightEdge.x; // Respawn al lado derecho
            transform.position = position;
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection; // Establece la velocidad
        if(direction.x>=0){
            spriteRenderer.flipX=false;
        }else{
            spriteRenderer.flipX=true;
        }
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // Establece la velocidad
    }
}
