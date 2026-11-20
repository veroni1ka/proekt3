using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float fallSpeed = 4f;   
    public float killY = -10f;     

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;      
    }

    void Start()
    {
        rb.linearVelocity = Vector2.down * fallSpeed;
    }

    public void SetSpeed(float newSpeed)
    {
        fallSpeed = newSpeed;
        rb.linearVelocity = Vector2.down * fallSpeed;
    }

    void Update()
    {
        // doshlo do ground aka dodged 
        if (transform.position.y < killY)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // destroy player ako ima collider on it
        GameObject player = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;
        Destroy(player);

        FindFirstObjectByType<CountdownTimer>().GameOver();
    }
}