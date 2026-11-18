using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Collectable : MonoBehaviour
{
    public float fallSpeed = 4f;   // units per second (bigger = faster)
    public float killY = -10f;     // destroy self when below this height

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;      // we control the speed ourselves
    }

    private void Start()
    {
        rb.linearVelocity = new Vector2(0f, -fallSpeed);
    }

    // called by the spawner so each round can be faster
    public void SetSpeed(float speed)
    {
        fallSpeed = speed;
        rb.linearVelocity = new Vector2(0f, -fallSpeed);
    }

    private void Update()
    {
        if (transform.position.y < killY)
        {
            Destroy(gameObject);   // dodged it
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player touched " + gameObject.name);

        GameObject target = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;

        Destroy(target);   // game over
    }
}