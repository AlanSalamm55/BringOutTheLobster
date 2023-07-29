using System;
using DefaultNamespace;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.GetKitchenObject() == null)
        {
            return;
        }

        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            if (player.HasKitchenObject())
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}