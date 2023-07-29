using System;

using DefaultNamespace;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    public event EventHandler OnPlayerGrabObject;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObjects.SpawnKithcenObject(kitchenObjectSo, player);
            
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }
}