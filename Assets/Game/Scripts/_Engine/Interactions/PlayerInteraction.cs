using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        Debug.Log("INT");
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}
