using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            ContactWithPlayer(other);
        }
    }

    protected abstract void ContactWithPlayer(Collider other);
}
