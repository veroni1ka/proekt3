using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float fallSpeed = 4f;   // bigger number = faster fall
    public float killY = -10f;     // below this height the fireball is removed

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Start()
    {
        rb.linearVelocity = new Vector2(0f, -fallSpeed);
    }

    // the spawner calls this to make the fireball faster
    public void SetSpeed(float newSpeed)
    {
        fallSpeed = newSpeed;
        rb.linearVelocity = new Vector2(0f, -fallSpeed);
    }

    void Update()
    {
        // fell off the bottom = the player dodged it
        if (transform.position.y < killY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Fireball hit the player!");

        // remove the whole player
        GameObject target = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;
        Destroy(target);

        // tell the timer the game is over
        FindFirstObjectByType<CountdownTimer>().GameOver();
    }
}