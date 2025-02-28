using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float minX = -0.9469f;
    public float minY = -0.68238f;
    public float maxX = 0.0531f;
    public float maxY = 0.44238f;
    public float minSpeed = 0.04f;
    public float maxSpeed = 7.0f;
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
        body = GetComponent<Rigidbody2D>();
        targetPostion = getRandomPosition();
        initialPosition();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        Vector2 currentPos = body.position;
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
                if(target != null)
                {
                    targetPostion = target.transform.position;
                }

                Debug.Log("Launching towards: " + targetPostion);
                launch();
            }
        }

        float distance = Vector2.Distance(currentPos, targetPostion);


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
            Vector2 direction = (targetPostion - currentPos).normalized;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPostion, currentSpeed);
            body.MovePosition(newPosition);
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

        transform.position = getRandomPosition();
        targetPostion = getRandomPosition();
        launching = false;
        rerouting = true;
    }
    public void launch()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned in BallBehavior!");
            return;
        }

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
            //Vector2 contactNormal = collision.GetContact(0).normal;

            //body.MovePosition(body.position + Vector2.Reflect(body.position, contactNormal));
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
            Vector2 contactNormal = collision.GetContact(0).normal;
        }
       
    }

    public void Reroute(Collision2D collision)
    {
        GameObject otherBall = collision.gameObject;
        Rigidbody2D otherBody = otherBall.GetComponent<Rigidbody2D>();

        if(rerouting)
        {
            rerouting = false;
            otherBody.GetComponent<BallBehavior>().rerouting = false;

            Vector2 contactNormal = collision.GetContact(0).normal;

            Vector2 newDirection = (contactNormal + Random.insideUnitCircle * 0.5f).normalized;
            targetPostion = body.position + newDirection * 1.5f;

            
            float separationDistance = 0.3f;
            body.position += contactNormal * separationDistance;
            otherBody.position -= contactNormal * separationDistance;

            otherBody.GetComponent <BallBehavior>().targetPostion = otherBody.position + 
                (-contactNormal + Random.insideUnitCircle * 0.5f).normalized * 1.5f;
        }
        else
        {
            rerouting = true;
        }
    }
}