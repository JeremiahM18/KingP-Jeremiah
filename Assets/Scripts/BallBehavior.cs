using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float minX = -7.0f;
    public float minY = -6.0f;
    public float maxX = 7.0f;
    public float maxY = 6.0f;
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
    public float timeLaunchStart;

    Rigidbody2D body;
    public bool rerouting;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPostion = getRandomPosition();
        initialPosition();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        Vector2 currentPos = gameObject.GetComponent<Transform>().position;

        Vector2 currentPosition = body.position;

        if (!onCooldown())
        {
            if (launching)
            {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if(currentLaunchTime > launchDuration)
                {
                    Debug.Log("Cooldown started.");
                    startCooldown();
                }
            } else
            {
                Debug.Log("Launching towards: " + targetPostion);
                launch();
            }
        }

        float distance = Vector2.Distance(currentPosition, targetPostion);


        if (distance > 0.1f)
        {
            float difficulty = getDifficultyPercentage();
            float currentSpeed;

            if (launching == true)
            {
                float launchingForHowLong = Time.time - timeLaunchStart;
                if (launchingForHowLong > launchDuration)
                {
                    startCooldown();
                }
                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, difficulty);
            }
            else
            {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            }
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPostion, currentSpeed);
            transform.position = newPosition;
        }
        else
        {   //     You are at target
            if (launching == true)
            {
                startCooldown();
            }
            targetPostion = getRandomPosition();
        }
    }

    Vector2 getRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randomX, randomY);
        return v;

    }

    public float getDifficultyPercentage()
    {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
        return difficulty;
    }

    public void initialPosition()
    {
        body = GetComponent<Rigidbody2D>();
        body.position = getRandomPosition();
        targetPostion = getRandomPosition();
        launching = false;
        rerouting = true;
    }
    public void launch()
    {

        //targetPostion = target.transform.position;
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPostion = targetBody.position;

        if (launching == false)
        {
            timeLaunchStart = Time.time;
            launching = true;

        }
    }

    public void setBounds(float miX, float maX, float miY,  float maY)
    {
        minX = miX; 
        maxX = maX; 
        minY = miY;
        maxY = maY;
    }

    public void setTarget(GameObject pin)
    {
        target = pin;
        if (target != null)
        {
            targetPostion = target.transform.position;
        }
    }
    public bool onCooldown()
    {
        bool result = false;

        float timeSinceLastLaunch = Time.time - timeLastLaunch;

        if (timeSinceLastLaunch < cooldown)
        {
            result = true;
        }


        return result;
    }

    public void startCooldown()
    {
        timeLastLaunch = Time.time;
        launching = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this + " Collided with: " + collision.gameObject.tag);
        if(collision.gameObject.tag == "Wall")
        {
            targetPostion = getRandomPosition();
        }
        if(collision.gameObject.tag == "Ball")
        {
            Reroute(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(this + " Collided with: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Wall")
        {
            targetPostion = getRandomPosition();
        }
       
    }

    public void Reroute(Collision2D collision)
    {
        GameObject otherBall = collision.gameObject;
        if(rerouting == true)
        {
            otherBall.GetComponent<BallBehavior>().rerouting = false;

            Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            Vector2 direction = targetPostion - body.position;

            targetPostion = Vector2.Reflect(direction, contact) + (contact * 0.5f);

            launching = false;
            float separationDistance = 0.3f;
            ballBody.position += contact * separationDistance;

        }
        else
        {
            rerouting = true;
        }
    }

}