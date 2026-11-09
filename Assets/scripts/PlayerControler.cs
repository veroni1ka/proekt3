using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    private bool jumpPressed;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }

        //Debug.Log(inputHorizontal);
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
    }
    
}

