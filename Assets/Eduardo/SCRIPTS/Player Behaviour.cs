using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Vidas del perro
    private int dogLives = 3;
    //Boleano para identificar si el perro se puede mover
    private bool canMove, allowMove = true;
    //Referencia al New Input System
    private DogInputActions dogInputActions;
    //Espacio pata añadir el respawn
    public GameObject respawnPoint;
    public MusicManager musicManager;

    //Campo para añadir el UI manager
    [SerializeField] UImanager uIManager;
    //Campo para poner las unidades en las que se mueve
    [SerializeField] protected float lateralMovementUnits = 0.25f;

    [SerializeField] protected Animator animator;
    [SerializeField] protected ChangeSceneManager gameOverManager;

    protected SlowDownLineSkill slowDownTrigger;
    private void Awake() //Esto es necesario para que jale el input system
    {
        dogInputActions = new DogInputActions();
    }
    private void OnEnable() //Esto es necesario para que jale el input system
    {
        dogInputActions.Enable();
    }
    private void OnDisable() //Esto es necesario para que jale el input system
    {
        dogInputActions.Disable();
    }
    void Start()
    {
        canMove = true;
        allowMove = true;
        slowDownTrigger = GetComponent<SlowDownLineSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        //Codigo de dirección de movimiento
        if (allowMove&&canMove)
        {
            CheckMovementDirections();
            Vector3 movement = Vector3.zero;

            if(canMoveUp && dogInputActions.Base.MoveUp.WasPressedThisFrame()) //if (canMoveUp && Input.GetKeyDown(KeyCode.UpArrow)) <- Este es el anterior
            {
                movement = Vector3.up;
                animator.SetBool("IsVerticalAxis", true);
                animator.SetBool("IsInPositiveDirection", true);
            }
            if(canMoveDown && dogInputActions.Base.MoveDown.WasPressedThisFrame()) //if (canMoveDown && Input.GetKeyDown(KeyCode.DownArrow)) <- Este es el anterior
            {
                movement = Vector3.down;
                animator.SetBool("IsVerticalAxis", true);
                animator.SetBool("IsInPositiveDirection", false);
            }
            if(canMoveLeft && dogInputActions.Base.MoveLeft.WasPressedThisFrame()) //if (canMoveLeft && Input.GetKeyDown(KeyCode.LeftArrow)) <- Este es el anterior
            {
                movement = Vector3.left;
                animator.SetBool("IsVerticalAxis", false);
                animator.SetBool("IsInPositiveDirection", false);
            }
            if(canMoveRight && dogInputActions.Base.MoveRight.WasPressedThisFrame()) //if (canMoveRight && Input.GetKeyDown(KeyCode.RightArrow)) <- Este es el anterior
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

    public float rayDistance = 1.25f; // Distancia del rayo para detectar colisiones.
    public LayerMask obstacleLayer; // Asigna el Layer de los obstáculos.

    // Estos bools indicarán si te puedes mover en cada dirección.
    private bool canMoveUp;
    private bool canMoveDown;
    private bool canMoveLeft;
    private bool canMoveRight;
    void CheckMovementDirections()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;
        // Rayos hacia arriba, abajo, izquierda y derecha.
        canMoveUp = !Physics2D.Raycast(position, Vector2.up, rayDistance, obstacleLayer);
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
            if (dogLives != 0)
            {
                musicManager.PlayCarSound();
            }

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
        }

        if (other.CompareTag("Cat"))
        {
            slowDownTrigger.MoveSlowDownTrigger();
            animator.SetBool("HasCat", true);
            respawnPoint.transform.position = other.transform.position;
        }
        if (other.CompareTag("Bone"))
        {
            slowDownTrigger.ResetUses();
            Destroy(other.gameObject);
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
        StopMovement();
        musicManager.PlayDeadMusic();
        musicManager.backGroundMusic.Stop();
        yield return new WaitForSeconds(4f);
        gameOverManager.Activate();
    }
    public void StopMovement()
    {
        allowMove = false;
    }
}
