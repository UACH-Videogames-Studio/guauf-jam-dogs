using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour
{
    public MusicManager musicManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Dog"))
        {
            other.GetComponent<PlayerBehaviour>().StopMovement();

            Destroy(this.gameObject);
        }
    }
}
