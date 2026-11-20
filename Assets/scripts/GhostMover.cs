using UnityEngine;

public class GhostMover : MonoBehaviour
{
    public float speed = 2f;

    private float leftX = -7f;
    private float rightX = 7f;
    private int direction = 1;   
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // called by the spawner right after it creates the ghost
    public void Setup(float left, float right)
    {
        leftX = left;
        rightX = right;
        direction = Random.value < 0.5f ? -1 : 1;
    }

    void Update()
    {
        //da odi levo || desno
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        // turn around 
        if (transform.position.x >= rightX) direction = -1;
        if (transform.position.x <= leftX) direction = 1;

        // facing the way it walks
        if (sr != null) sr.flipX = direction < 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // destroy player ako colliderot e on it
        GameObject player = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;
        Destroy(player);

        FindFirstObjectByType<CountdownTimer>().GameOver();
    }
}