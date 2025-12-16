using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;

    private float ghostDelay = 0.1f;
    private float ghostDelaySeconds;
    [SerializeField] private GameObject ghost;

    public bool makeGhost;

    private void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    private void Update()
    {
        // Enable ghost only while slow-mo is active
        makeGhost = manager.shieldHandler.isShielded;

        if (!makeGhost) return;
        if (!manager.movement.isMoving) return;

        if (ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.unscaledDeltaTime; // OR deltaTime
        }
        else
        {
            SpawnGhost();
        }
    }

    private void SpawnGhost()
    {
        GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
        Sprite currentSprite = manager.spriteRenderer.sprite;
        currentGhost.transform.localPosition = transform.localPosition;
        SpriteRenderer sr = currentGhost.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = currentSprite;
        sr.flipX = manager.spriteRenderer.flipX;
        ghostDelaySeconds = ghostDelay;
        Destroy(currentGhost, .5f);
    }
}
