using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Collectable : MonoBehaviour
{
    public float fallSpeed = 1f;   // grav pull down

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checkin
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player touched " + gameObject.name);

        Destroy(other.gameObject);
    }
}