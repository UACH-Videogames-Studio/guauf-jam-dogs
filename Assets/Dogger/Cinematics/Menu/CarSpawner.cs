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
    float carSpeed=5;
    [SerializeField]
    bool carToRight=true;
    [SerializeField]
    private GameObject carPrefab;

    [SerializeField]
    private CarMovement[]  defaultCars;

    [SerializeField]
    private float gapBetweenCars = 0.5f;

    [SerializeField]
    private float minTime, maxTime = 0f;
    private float lastSpawnX;

    private Vector3 positionLastSpawned;

    Coroutine timeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        carsLength = cars.Length;
        lastSpawnX = transform.position.x;

        if(defaultCars!=null){
            for(int i=0; i<defaultCars.Length; i++){
                defaultCars[i].speed=(carToRight?1:-1)*carSpeed;
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
        GameObject newCar = Instantiate(carPrefab, new Vector3(lastSpawnX, transform.position.y, transform.position.z), transform.rotation, transform);

        newCar.GetComponent<CarMovement>().speed=(carToRight?1:-1)*carSpeed;

        // Pick a random sprite from the array of car sprites
        Sprite randomCarSprite = cars[Random.Range(0, carsLength)];

        // Set the sprite of the car's SpriteRenderer to the random sprite
        SpriteRenderer carSpriteRenderer = newCar.GetComponent<SpriteRenderer>();
        if (carSpriteRenderer != null)
        {
            carSpriteRenderer.sprite = randomCarSprite;

            // Calculate the width of the car sprite
            float carWidth = carSpriteRenderer.bounds.size.x;
            
            Vector3 newCarPosition=newCar.transform.position;
            // Update the last spawn X position by adding the car's width and the gap between cars
            if(carToRight){
                newCar.transform.position=new Vector3(Mathf.Min(positionLastSpawned.x - (carWidth + gapBetweenCars), newCarPosition.x), newCarPosition.y);
            }else{
                newCar.transform.position=new Vector3(Mathf.Min(positionLastSpawned.x - (carWidth + gapBetweenCars), newCarPosition.x), newCarPosition.y);
            }
            positionLastSpawned = newCarPosition;
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
