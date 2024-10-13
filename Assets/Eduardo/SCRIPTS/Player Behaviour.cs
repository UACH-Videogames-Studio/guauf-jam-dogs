using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private int dogLives = 3;
    private bool canMove = true;
    public GameObject respawnPoint;

    [SerializeField] UImanager uIManager;
    [SerializeField] protected float lateralMovementUnits = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Damague();
        }
        if (canMove)
        {
            Vector3 movement = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                movement = Vector3.up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                movement = Vector3.down;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movement = Vector3.left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                movement = Vector3.right;
            }
            if (movement != Vector3.zero)
            {
                dogMovement(movement);
            }
        }
    }
    private void Damague ()
    {
        if (dogLives > 0)
        {
            dogLives--;
            uIManager.LessHeart(dogLives);
            Respawn();

            if (dogLives == 0)
            {
                Console.WriteLine("Hemos muerto");
                Debug.Log("Hemos muerto");
            }
        }
    } 

    private void dogMovement (Vector3 direction)
    {
        canMove = false;

        transform.position += direction * lateralMovementUnits;

        StartCoroutine(WaitToMove());
    }

    System.Collections.IEnumerator WaitToMove ()
    {
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            Damague();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.transform.position;
    }
}
