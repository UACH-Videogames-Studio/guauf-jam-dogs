using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Vidas del perro
    private int dogLives = 3;
    //Boleano para identificar si el perro se puede mover
    private bool canMove = true;
    //Espacio pata añadir el respawn
    public GameObject respawnPoint;
    public MusicManager musicManager;

    //Campo para añadir el UI manager
    [SerializeField] UImanager uIManager;
    //Campo para poner las unidades en las que se mueve
    [SerializeField] protected float lateralMovementUnits = 0.25f;

    [SerializeField] protected Animator animator;
    [SerializeField] protected ChangeSceneManager levelManager;
    [SerializeField] protected ChangeSceneManager gameOverManager;

    protected SlowDownLineSkill slowDownTrigger;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        slowDownTrigger=GetComponent<SlowDownLineSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementDirections();
        //Codigo para bajar vidas con la K
        if (Input.GetKeyDown(KeyCode.K))
        {
            Damague();
        }
        //Codigo de dirección de movimiento
        if (canMove)
        {
            Vector3 movement = Vector3.zero;

            if (canMoveUp && Input.GetKeyDown(KeyCode.UpArrow))
            {
                movement = Vector3.up;
                animator.SetBool("IsVerticalAxis", true);
                animator.SetBool("IsInPositiveDirection", true);
            }
            if (canMoveDown && Input.GetKeyDown(KeyCode.DownArrow))
            {
                movement = Vector3.down;
                animator.SetBool("IsVerticalAxis", true);
                animator.SetBool("IsInPositiveDirection", false);
            }
            if (canMoveLeft && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movement = Vector3.left;
                animator.SetBool("IsVerticalAxis", false);
                animator.SetBool("IsInPositiveDirection", false);
            }
            if (canMoveRight && Input.GetKeyDown(KeyCode.RightArrow))
            {
                movement = Vector3.right;
                animator.SetBool("IsVerticalAxis", false);
                animator.SetBool("IsInPositiveDirection", true);
            }
            if (movement != Vector3.zero)
            {
                dogMovement(movement);
            }
        }
    }

    public float rayDistance = 1f; // Distancia del rayo para detectar colisiones.
    public LayerMask obstacleLayer; // Asigna el Layer de los obstáculos.

    // Estos bools indicarán si te puedes mover en cada dirección.
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;
    void CheckMovementDirections()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;
        // Rayos hacia arriba, abajo, izquierda y derecha.
        canMoveUp = !Physics2D.Raycast(position, Vector2.up, 2, obstacleLayer);
        canMoveDown = !Physics2D.Raycast(position, Vector2.down, rayDistance, obstacleLayer);
        canMoveLeft = !Physics2D.Raycast(position, Vector2.left, rayDistance, obstacleLayer);
        canMoveRight = !Physics2D.Raycast(position, Vector2.right, rayDistance, obstacleLayer);

        // Opcional: visualización de los rayos en el Editor.
        Debug.DrawRay(position, Vector2.up * rayDistance, canMoveUp ? Color.green : Color.red);
        Debug.DrawRay(position, Vector2.down * rayDistance, canMoveDown ? Color.green : Color.red);
        Debug.DrawRay(position, Vector2.left * rayDistance, canMoveLeft ? Color.green : Color.red);
        Debug.DrawRay(position, Vector2.right * rayDistance, canMoveRight ? Color.green : Color.red);
    }

    //Codigo para manejar el daño
    private void Damague()
    {
        if (dogLives > 0)
        {
            dogLives--;
            uIManager.LessHeart(dogLives);
            Respawn();

            if (dogLives == 0)
            {
                StartCoroutine(TimeBeforeGameOver());
            }
        }
    }

    //Movimiento del personaje
    private void dogMovement(Vector3 direction)
    {
        canMove = false;

        transform.position += direction * lateralMovementUnits;

        StartCoroutine(WaitToMove());
    }

    //Codigo para esperar para otro movimiento
    System.Collections.IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }

    //Detectar collider para activar daño
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            Damague();
            musicManager.PlayCarSound();
        }

        if (other.CompareTag("Cat"))
        {
            slowDownTrigger.MoveSlowDownTrigger();
            animator.SetBool("HasCat", true);
            respawnPoint.transform.position = other.transform.position;
        }
    }

    //Respawner
    private void Respawn()
    {
        transform.position = respawnPoint.transform.position;
    }

    IEnumerator TimeBeforeGameOver()
    {
        Debug.Log("Hemos muerto");
        canMove = false;
        musicManager.PlayDeadMusic();
        musicManager.backGroundMusic.Stop();
        yield return new WaitForSeconds(5);
        gameOverManager.Activate();
    }
    IEnumerator TimeBeforeNextLevel()
    {
        yield return new WaitForSeconds(5);
        gameOverManager.Activate();
    }
}
