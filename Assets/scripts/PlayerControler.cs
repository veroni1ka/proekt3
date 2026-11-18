using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    private bool jumpPressed;

    private Rigidbody2D rb;
    private float inputHorizontal;
    private float startScaleX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startScaleX = Mathf.Abs(transform.localScale.x);
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        // face the way we walk by mirroring the whole character
        if (inputHorizontal > 0)
        {
            SetFacing(-1);     // D = right
        }
        else if (inputHorizontal < 0)
        {
            SetFacing(1);    // A = left
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    private void SetFacing(int dir)
    {
        Vector3 s = transform.localScale;
        s.x = startScaleX * dir;
        transform.localScale = s;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputHorizontal * moveSpeed, rb.linearVelocity.y);

        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            jumpPressed = false;
        }
    }
}