using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    private Rigidbody2D rb;
    private float moveInput;
    private bool wantsToJump;
    private float startScaleX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startScaleX = Mathf.Abs(transform.localScale.x);
    }

    void Update()
    {
        // A = -1, D = +1, nothing = 0
        moveInput = Input.GetAxisRaw("Horizontal");

        // turn the whole character to face the way we walk
        if (moveInput > 0) Face(-1);   // D (right)
        if (moveInput < 0) Face(1);    // A (left)

        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantsToJump = true;
        }
    }

    void Face(int direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = startScaleX * direction;
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        // walk left or right, keep the up/down speed
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (wantsToJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            wantsToJump = false;
        }
    }
}