using UnityEngine;

public class SpawningBehaviour : MonoBehaviour
{

    public GameObject[] ballVariants;
    public GameObject[] targetObject;
    GameObject newObject;
    public float startTime;
    public float spawnRatio = 1.0f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        float timeElapsed = currentTime - startTime;
        if (timeElapsed > spawnRatio)
        {
            spawnBall();
            startTime = Time.time;
        }
    }

    void spawnBall()
    {
        int numVariants = ballVariants.Length;
        if (numVariants > 0)
        {
            int selection = Random.Range(0, numVariants);
            newObject = Instantiate(ballVariants[selection], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            BallBehavior ballBehavior = newObject.GetComponent<BallBehavior>();
            ballBehavior.setBounds(minX, maxX, minY, maxY);

            if (targetObject.Length > 0)
            {
                int targetIndex = Random.Range(0, targetObject.Length);
                ballBehavior.setTarget(targetObject[targetIndex]);
            }

            ballBehavior.initialPosition();
        }

        startTime = Time.time;
    }
}
