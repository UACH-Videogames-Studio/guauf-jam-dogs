using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float VelMov;
    float velX, velY;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velX = Input.GetAxisRaw("Horizontal")*VelMov;
        velY = Input.GetAxisRaw("Vertical")*VelMov;
        rb.velocity = new Vector2(velX, velY);
    }

}
