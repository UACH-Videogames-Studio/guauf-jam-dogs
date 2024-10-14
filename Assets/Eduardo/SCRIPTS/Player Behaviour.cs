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

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //Codigo para bajar vidas con la K
        if (Input.GetKeyDown(KeyCode.K))
        {
            Damague();
        }
        //Codigo de dirección de movimiento
        if (canMove)
        {
            Vector3 movement = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                movement = Vector3.up;
                animator.SetBool("IsVerticalAxis", true);
                animator.SetBool("IsInPositiveDirection", true);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                movement = Vector3.down;
                animator.SetBool("IsVerticalAxis", true);
                animator.SetBool("IsInPositiveDirection", false);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movement = Vector3.left;
                animator.SetBool("IsVerticalAxis", false);
                animator.SetBool("IsInPositiveDirection", false);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
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

    //Codigo para manejar el daño
    private void Damague ()
    {
        if (dogLives > 0)
        {
            dogLives--;
            uIManager.LessHeart(dogLives);
            
            Respawn();
            if (dogLives != 0)
            {
                musicManager.PlayCarSound();
            }    
            
            if (dogLives == 0)
            {
                Debug.Log("Hemos muerto");
                Time.timeScale = 0;
                musicManager.PlayDeadMusic();
                musicManager.backGroundMusic.Stop();
            }
        }
    } 

    //Movimiento del personaje
    private void dogMovement (Vector3 direction)
    {
        canMove = false;

        transform.position += direction * lateralMovementUnits;

        StartCoroutine(WaitToMove());
    }

    //Codigo para esperar para otro movimiento
    System.Collections.IEnumerator WaitToMove ()
    {
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }

    //Detectar collider para activar daño
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            Damague();
            
        }

        if (other.CompareTag ("Cat"))
        {
            respawnPoint.transform.position = other.transform.position;
        }
    }

    //Respawner
    private void Respawn()
    {
        transform.position = respawnPoint.transform.position;
    }
}
