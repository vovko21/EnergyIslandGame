using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetIdle()
    {
        if (_animator == null)
        {
            return;
        }

        _animator.SetBool("isWalking", false);
    }

    public void SetWalk()
    {
        if (_animator == null)
        {
            return;
        }

        _animator.SetBool("isWalking", true);
    }
}
