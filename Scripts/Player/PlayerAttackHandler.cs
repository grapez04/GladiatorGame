using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private PlayerManager manager;


    [SerializeField] private LayerMask enemyLayer;
    [HideInInspector] public float radius;

    [Header("Detection Angle Radius")]
    private float detectionAngle = 120f;
    [SerializeField] private float detectionRadius = 1.7f;

    [Space]
    [SerializeField] private Collider2D[] hitColliders;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        FindAttackableEnemies();
    }

    void FindAttackableEnemies()
    {
        Vector2 origin = transform.position;

        // Step 1: detect all enemies in radius
        Collider2D[] all = Physics2D.OverlapCircleAll(origin, detectionRadius, enemyLayer);

        Vector2 aimDir = manager.movement.aimDirection.normalized;

        // Step 2: store only those inside cone
        var insideCone = new System.Collections.Generic.List<Collider2D>();

        foreach (Collider2D col in all)
        {
            Vector2 dirToTarget = (Vector2)col.transform.position - origin;
            float angle = Vector2.Angle(aimDir, dirToTarget);

            if (angle <= detectionAngle * 0.5f)
            {
                insideCone.Add(col);
            }
        }

        // Only enemies inside cone
        hitColliders = insideCone.ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (manager != null)
        {
            // Draw the angle cone
            Vector3 origin = transform.position;
            Vector2 aimDir = manager.movement.aimDirection.normalized;

            // Left boundary
            Vector3 leftDir = Quaternion.Euler(0, 0, detectionAngle * 0.5f) * aimDir;
            // Right boundary
            Vector3 rightDir = Quaternion.Euler(0, 0, -detectionAngle * 0.5f) * aimDir;

            Gizmos.DrawLine(origin, origin + leftDir * detectionRadius);
            Gizmos.DrawLine(origin, origin + rightDir * detectionRadius);
        }
    }
}
