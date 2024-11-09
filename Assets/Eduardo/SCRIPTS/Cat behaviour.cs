using System.Collections;
using UnityEngine;



public class CatBehaviour : MonoBehaviour
{
    public MusicManager musicManager;

    public float moveDuration = 3f; // Tiempo que durará el movimiento
    public Vector3 moveOffset = new Vector3(0f, 5f, 0f); // Distancia que se moverá el gato
    private bool isMoving = false; // Para evitar que se inicie varias veces el movimiento
    
    public AudioClip catsound;
    public AudioClip catpain;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Método para que el gato se mueva y luego desaparezca
    public void StartDisappearingAnimation()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveAndDisappear());
        }
    }

    // Corrutina que mueve al gato y luego lo desactiva
    private IEnumerator MoveAndDisappear()
    {
        
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + moveOffset; // Movimiento final

        float elapsedTime = 0f;
        if (catsound != null && !audio.isPlaying)
        {
            audio.clip = catsound;
            audio.Play();
        }
        // Mueve al gato hacia la posición final
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dog"))
        {
            Destroy(gameObject); // Si el perro toca al gato, destruirlo para cambiarlo por la animacion del perro con el gato
            musicManager.PlayCatSound();
        }
        if (other.CompareTag("Car"))
        {
            if (audio.isPlaying)
            {
                audio.Stop();
                
            }
            audio.PlayOneShot(catpain);
            
            StartCoroutine(waitForSound());
        }
    }

    private IEnumerator waitForSound()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
