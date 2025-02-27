using UnityEngine;

public class SpawningBehaviour : MonoBehaviour
{

    public GameObject[] ballVariants;
    public GameObject targetObject;
    GameObject newObject;
    public float startTime;
    public float spawnRatio = 1.0f;
    public Pins pinsDB;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 3.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPin();
        Invoke("spawnBall", spawnRatio);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    float currentTime = Time.time;
    //    float timeElapsed = currentTime - startTime;
    //    if (timeElapsed > spawnRatio)
    //    {
    //        spawnBall();
    //        spawnRatio = Random.Range(minSpawnTime, maxSpawnTime);
    //    }
    //}

    void spawnBall()
    {
        int numVariants = ballVariants.Length;
        if (numVariants > 0)
        {
            int selection = Random.Range(0, numVariants);
            Vector2 spawnPos = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            newObject = Instantiate(ballVariants[selection], spawnPos, Quaternion.identity);
            BallBehavior ballBehavior = newObject.GetComponent<BallBehavior>();
           
            ballBehavior.setBounds(minX, maxX, minY, maxY);
            ballBehavior.setTarget(targetObject);
            ballBehavior.initialPosition();
        }

        float nextSpawnTime = Random.Range(minSpawnTime,minSpawnTime);
        Invoke("spawnBall", nextSpawnTime);
    }

    void spawnPin()
    {
        Pin selectedPin = pinsDB.getPin(CharacterManager.selection);
        if (selectedPin != null)
        {
            if(targetObject != null)
            {
                targetObject = Instantiate(selectedPin.prefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
            }
        }
    }
}
