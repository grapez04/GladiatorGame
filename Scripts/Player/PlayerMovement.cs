using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public MouseHandler mouseHandler;
    [SerializeField] private Transform aim;

    private Rigidbody2D rb;
    public Vector2 move;

    [HideInInspector] public float movespeed = 5f;

    public Vector2 aimDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mouseHandler = GetComponent<MouseHandler>();
    }

    private void Update()
    {
        RotateAim();
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
        Vector3 mouseWorldPos = mouseHandler.cursor.position;

        aimDirection = (mouseWorldPos - aim.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aim.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
