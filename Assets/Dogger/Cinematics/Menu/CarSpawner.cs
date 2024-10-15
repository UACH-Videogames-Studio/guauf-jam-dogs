using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private Sprite[] cars;
    private int carsLength = 0;
    [SerializeField]
    [Min(0)]
    float carSpeed = 5;
    [SerializeField]
    private GameObject carPrefab;

    [SerializeField]
    private CarMovement[] defaultCars;

    [SerializeField]
    private float gapBetweenCars = 0.5f;

    [SerializeField]
    private float minTime, maxTime = 0f;

    [SerializeField] private GameObject lastSpawned;

    Coroutine timeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        carsLength = cars.Length;

        if (defaultCars != null)
        {
            for (int i = 0; i < defaultCars.Length; i++)
            {
                defaultCars[i].speed = carSpeed;
            }
        }

        // Start the coroutine to spawn cars at random intervals
        timeCoroutine = StartCoroutine(TimeCoroutine());
    }

    void OnDisable()
    {
        StopCoroutine(timeCoroutine);
    }

    void SpawnCar()
    {
        // Instantiate a new car prefab at the spawner's position and rotation
        GameObject newCar = Instantiate(carPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation, transform);

        newCar.GetComponent<CarMovement>().speed = carSpeed;

        // Pick a random sprite from the array of car sprites
        Sprite randomCarSprite = cars[Random.Range(0, carsLength)];

        // Set the sprite of the car's SpriteRenderer to the random sprite
        SpriteRenderer carSpriteRenderer = newCar.GetComponent<SpriteRenderer>();
        if (carSpriteRenderer != null)
        {
            carSpriteRenderer.sprite = randomCarSprite;

            // Calculate the width of the car sprite
            float carWidth = carSpriteRenderer.bounds.size.x;

            Vector3 newCarPosition = newCar.transform.position;
            float x1=newCarPosition.x;
            float x2Position = lastSpawned.transform.position.x-carWidth-gapBetweenCars;
            if (x1 > x2Position){
                Debug.Log((gameObject.name, "Moved to avoid colission", x1, x2Position));
                x1=x2Position;
            }
            newCar.transform.position = new Vector3(x1, newCarPosition.y);
            lastSpawned = newCar;
        }
    }

    IEnumerator TimeCoroutine()
    {
        while (true)
        {
            // Wait for a random amount of time between minTime and maxTime
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            SpawnCar();
        }
    }
}
