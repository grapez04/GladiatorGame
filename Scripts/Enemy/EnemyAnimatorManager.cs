using UnityEngine;

public class EnemyAnimatorManager : MonoBehaviour
{
    private EnemyManager manager;

    public Animator animator;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    public void Initialize()
    {
        animator.Rebind();
    }

    private void Update()
    {
        AnimateMovement();
    }

    private void AnimateMovement()
    {
        if (manager.isMoving)
        {
            animator.SetBool("Move", true);
        }
        else animator.SetBool("Move", false);
    }

    public void PlayTargetAnim(string anim)
    {
        animator.Play(anim);
    }
}
