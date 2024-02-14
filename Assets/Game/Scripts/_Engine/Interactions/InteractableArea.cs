using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableArea : MonoBehaviour
{
    protected bool _isPlayerIn;

    public event Action<bool> OnPlayerTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            _isPlayerIn = true;
            ContactWithPlayer(other);
            OnPlayerTrigger?.Invoke(_isPlayerIn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            _isPlayerIn = false;
            PlayerExit(other);
            OnPlayerTrigger?.Invoke(_isPlayerIn);
        }
    }

    protected abstract void ContactWithPlayer(Collider other);
    protected abstract void PlayerExit(Collider other);
}
