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
        moveInput = Input.GetAxisRaw("Horizontal");

        // za kade da bide svrten playerot koa se dzvizhi(levo kje bide -/a, desno +/d)
        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Sign(moveInput) * startScaleX;
            transform.localScale = scale;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            wantsToJump = true;
    }

    void FixedUpdate()
    {
        // ist speed osven ako skokne
        float y = rb.linearVelocity.y;

        if (wantsToJump)
        {
            y = jumpHeight;
            wantsToJump = false;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, y);
    }
}