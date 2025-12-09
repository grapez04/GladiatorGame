using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Init(RuntimeAnimatorController newController)
    {
        animator.runtimeAnimatorController = newController;
    }

    public void AnimateMovement(Vector2 move)
    {
        if (move != Vector2.zero)
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
