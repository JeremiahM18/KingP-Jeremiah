using UnityEngine;

public class PinBehavior : MonoBehaviour
{

    public float speed = 4.0f;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime) ;
        transform.position = newPosition;
    }
}
