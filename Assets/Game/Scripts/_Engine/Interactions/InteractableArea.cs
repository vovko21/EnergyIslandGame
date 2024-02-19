using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableArea : MonoBehaviour
{
    protected bool _isCharacterIn;

    public event Action<bool> OnCharacterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            _isCharacterIn = true;
            ContactWithPlayer(other.GetComponent<Player>());
            OnCharacterTrigger?.Invoke(_isCharacterIn);
        }
        if(other.CompareTag(Constants.WORKER_TAG))
        {
            _isCharacterIn = true;
            ContactWithWorker(other.GetComponent<Worker>());
            OnCharacterTrigger?.Invoke(_isCharacterIn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            _isCharacterIn = false;
            PlayerExit(other.GetComponent<Player>());
            OnCharacterTrigger?.Invoke(_isCharacterIn);
        }
        if (other.CompareTag(Constants.WORKER_TAG))
        {
            _isCharacterIn = false;
            WorkerExit(other.GetComponent<Worker>());
            OnCharacterTrigger?.Invoke(_isCharacterIn);
        }
    }

    protected virtual void ContactWithPlayer(Player player) { }
    protected virtual void PlayerExit(Player player) { }

    protected virtual void ContactWithWorker(Worker worker) { }
    protected virtual void WorkerExit(Worker worker) { }
}
