using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    private bool jumpPressed;
    private bool isGrounded;

    private Rigidbody2D rb;
    private float inputHorizontal;
    //private float inputVertical;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        //inputVertical = Input.GetAxis("Vertical");

        Debug.Log(inputHorizontal);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded) jumpPressed = false;
            jumpPressed = true;
        }

        //Debug.Log(inputHorizontal);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // surface is mostly upward aka floor, and not already moving up
            if (contact.normal.y > 0.7f && rb.linearVelocity.y <= 0.1f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        float x = inputHorizontal * moveSpeed;
        float y = rb.linearVelocity.y;

        rb.linearVelocity = new Vector2(x, y);

        if (jumpPressed)
        {
            float xJump = rb.linearVelocity.x;
            rb.linearVelocity = new Vector2(xJump, jumpHeight);
            jumpPressed = false;
        }
        isGrounded = false;

    }
    
}

