using UnityEngine;

public class TakeItemArea : InteractableArea
{
    [SerializeField] private HandItemType _handItemType;

    protected override void ContactWithPlayer(Player player)
    {
        player.Hands.TakeItem(_handItemType);
    }
}
