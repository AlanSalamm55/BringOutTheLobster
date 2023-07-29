using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSo;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSoList;
    private List<KitchenObjectSO> kitchenObjectSoList;

    private void Awake()
    {
        kitchenObjectSoList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngrediet(KitchenObjectSO kitchenObjectSo)
    {
        if (!validKitchenObjectSoList.Contains(kitchenObjectSo))
        {
            return false;
        }

        if (kitchenObjectSoList.Contains(kitchenObjectSo))
        {
            return false;
        }
        else
        {
            kitchenObjectSoList.Add(kitchenObjectSo);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { KitchenObjectSo = kitchenObjectSo });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSoList()
    {
        return kitchenObjectSoList;
    }
}