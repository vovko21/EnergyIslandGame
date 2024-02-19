using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
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

    public void SetCarrying(bool carry)
    {
        if (_animator == null)
        {
            return;
        }

        if(carry)
        {
            _animator.SetLayerWeight(1, 1);
        }
        else
        {
            _animator.SetLayerWeight(1, 0);
        }
    }
}
