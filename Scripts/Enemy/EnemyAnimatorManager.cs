using UnityEngine;

public class EnemyAnimatorManager : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    public void AnimateMovement()
    {
        if (manager.isMoving)
        {
            animator.SetBool("Move", true);
        }
        else animator.SetBool("Move", false);
    }
}
