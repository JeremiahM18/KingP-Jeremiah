using Unity.VisualScripting;
using UnityEngine;

public class PinBehavior : MonoBehaviour
{

    public float start;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;
    Rigidbody2D body;

    public float baseSpeed = 2.0f;
    public float dashSpeed = 8.0f;
    public float dashDuration = 0.3f;
    public bool dashing;
    public float cooldownRate = 1.0f;
    public float endLastDash;
    public float cooldown = 0.0f;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        dashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        //newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime) ;
        //transform.position = newPosition;
    }
    public void FixedUpdate()
    {
        body = GetComponent<Rigidbody2D>();
        Vector2 currentPos = body.position;

        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(currentPos, mousePosG, speed * Time.fixedDeltaTime);
        body.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with " + collided);
        if (collided == "Ball" || collided == "Wall")
        {
            Debug.Log("Game Over");
        }
    }

    private void Dash()
    {
        if (dashing == true)
        {
            float currentTime = Time.time;
            float timeDashing = currentTime - start;
            if (timeDashing > dashDuration)
            {
                dashing = false;
                speed = baseSpeed;
                cooldown = cooldownRate;
            }
        }
        else
        {
            cooldown = cooldown - Time.deltaTime;
            if (cooldown < 0.0)
            {
                cooldown = 0.0f;
            }
            if (cooldown == 0.0 && Input.GetMouseButton(0))
            {
                dashing = true;
                speed = dashSpeed;
                start = Time.time;

            }

        }
    }
}