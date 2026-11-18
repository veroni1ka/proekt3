using UnityEngine;

public class GhostMover : MonoBehaviour
{
    public float speed = 2f;

    private float leftX = -7f;
    private float rightX = 7f;
    private float lifetime = 6f;
    private int direction = 1;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // called by the spawner right after it creates the ghost
    public void Setup(float left, float right, float life)
    {
        leftX = left;
        rightX = right;
        lifetime = life;
        direction = Random.value < 0.5f ? -1 : 1;
    }

    private void Update()
    {
        // move left/right only
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);

        if (transform.position.x >= rightX) direction = -1;
        if (transform.position.x <= leftX) direction = 1;

        // face the way it's moving (swap the < to > if it looks backwards)
        if (sr != null) sr.flipX = direction < 0;

        // disappear after a while so the round can end
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Ghost got the player");

        GameObject target = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;

        Destroy(target);   // game over
    }
}