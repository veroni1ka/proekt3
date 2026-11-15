using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Collectable : MonoBehaviour
{
    public float fallSpeed = 1f;   // grav pull down
    public float killY = -10f;     // kill koga e below hightot

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = fallSpeed;
    }

    private void Update()
    {
        // fell off screen, player dodging
        if (transform.position.y < killY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player touched " + gameObject.name);

        Destroy(other.gameObject);   // game over
    }
}