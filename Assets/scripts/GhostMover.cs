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

    // the spawner calls this right after it creates the ghost
    public void Setup(float left, float right)
    {
        leftX = left;
        rightX = right;
        direction = Random.value < 0.5f ? -1 : 1;
    }

    void Update()
    {
        // move left or right only
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);

        // turn around at the edges
        if (transform.position.x >= rightX) direction = -1;
        if (transform.position.x <= leftX) direction = 1;

        // face the way it moves (if it looks backwards, change < to >)
        if (sr != null) sr.flipX = direction < 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Ghost got the player");

        // remove the whole player
        GameObject target = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;
        Destroy(target);

        // tell the timer the game is over
        FindFirstObjectByType<CountdownTimer>().GameOver();
    }
}