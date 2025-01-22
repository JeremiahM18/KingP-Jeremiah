using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float minX = -8.01f;
    public float minY = -4.0f;
    public float maxX = 8.0f;
    public float maxY = 4.34f;
    public float minSpeed = 0.05f;
    public float maxSpeed = 15.0f;
    public Vector2 targetPostion;

    public int secondsToMaxSpeed;

    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPostion = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = gameObject.GetComponent<Transform>().position;
        //float distance = Vector2.Distance((Vector2)transform.position, targetPosition);
        if (targetPostion != currentPos)
        {
            float difficulty = getDifficultyPercentage();
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPostion, currentSpeed);
            transform.position = newPosition;
        }
        else
        {
            targetPostion = getRandomPosition();
        }
        getRandomPosition();
    }

    Vector2 getRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
        
    }

    public float getDifficultyPercentage()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }


}
