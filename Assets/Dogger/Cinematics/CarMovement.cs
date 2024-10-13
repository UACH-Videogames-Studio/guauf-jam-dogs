using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] public float speed=5;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    void Update(){
        rb.velocity=Vector2.right*speed;
    }
    
    void OnTriggerEnter2D(Collider2D collider){
        Destroy(gameObject);
    }
}
