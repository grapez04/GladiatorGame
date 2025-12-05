using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform aim;

    private Rigidbody2D rb;
    public Vector2 move;

    private Camera mainCamera;
    public Vector2 mousePos;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
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
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        Vector3 rotateDirection = (worldPosition - transform.position).normalized;
        rotateDirection.z = 0;

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        aim.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
