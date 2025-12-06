using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;
    [HideInInspector] public MouseHandler mouseHandler;
    [SerializeField] private Transform aim;

    private Rigidbody2D rb;
    public Vector2 move;

    [HideInInspector] public float movespeed = 5f;

    public Vector2 aimDirection;
    public Vector3 mouseWorldPos;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        mouseHandler = GetComponent<MouseHandler>();

        manager.spriteRenderer.flipX = false;
    }

    private void Update()
    {
        RotateAim();
        Flip();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.linearVelocity = move * movespeed;
    }

    private void RotateAim()
    {
        mouseWorldPos = mouseHandler.cursor.position;

        aimDirection = (mouseWorldPos - aim.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aim.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Flip()
    {
        if (mouseWorldPos.x < transform.position.x)
        {
            // Cursor is to the right -> not flipped
            manager.spriteRenderer.flipX = false;
        }
        else
        {
            // Cursor is to the left -> flipped
            manager.spriteRenderer.flipX = true;
        }
    }
}
