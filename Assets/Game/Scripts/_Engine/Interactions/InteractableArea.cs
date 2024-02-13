using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableArea : MonoBehaviour
{
    protected bool _isPlayerIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            _isPlayerIn = true;
            ContactWithPlayer(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            _isPlayerIn = false;
            PlayerExit(other);
        }
    }

    protected abstract void ContactWithPlayer(Collider other);
    protected abstract void PlayerExit(Collider other);
}
