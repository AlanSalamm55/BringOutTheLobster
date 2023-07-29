using System;
using DefaultNamespace;
using UnityEngine;
using ScriptableObjects;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
    private int cuttingProgress;

    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgress.OnprogressChangeEventArgs> OnProgressChange;
    public event EventHandler OnCutting;


    public new static void ResetStaticData()
    {
        OnAnyCut = null;
    }


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSo =
                        GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
                    OnProgressChange?.Invoke(this,
                        new IHasProgress.OnprogressChangeEventArgs
                            { progressNormalized = (float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax });
                }
            }
            else
            {
                //player has nothing
            }
        }
        else
        {
            //there is a kitchenobject
            if (player.HasKitchenObject())
            {
                //the player is carrying smth

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngrediet(GetKitchenObject().GetKitchenObjectSo()))
                        GetKitchenObject().DestroySelf();
                }
            }
            else
            {
                //player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlt(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSo()))
        {
            cuttingProgress++;

            CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
            OnProgressChange?.Invoke(this,
                new IHasProgress.OnprogressChangeEventArgs
                    { progressNormalized = (float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax });
            OnCutting?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            if (cuttingProgress >= cuttingRecipeSo.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
                GetKitchenObject().DestroySelf();
                KitchenObjects.SpawnKithcenObject(outputKitchenObjectSo, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSo);
        return cuttingRecipeSo != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSo);
        if (cuttingRecipeSo != null)
        {
            return cuttingRecipeSo.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipeSoArray)
        {
            if (cuttingRecipe.input == inputKitchenObjectSo)
            {
                return cuttingRecipe;
            }
        }

        return null;
    }
}