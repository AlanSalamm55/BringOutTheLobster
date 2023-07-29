using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompeleted;
    public event EventHandler OnRecipeSucess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSo;
    private List<RecipeSO> waitingRecipeSoList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipeDelivered;


    private void Awake()
    {
        Instance = this;
        waitingRecipeSoList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (recipeListSo.recipeSoList.Count > 0)
            {
                if (GameManager.Instance.IsGamePlaying() && waitingRecipeSoList.Count < waitingRecipesMax)
                {
                    int randomIndex = UnityEngine.Random.Range(0, recipeListSo.recipeSoList.Count);
                    RecipeSO waitingRecipeSo = recipeListSo.recipeSoList[randomIndex];
                    waitingRecipeSoList.Add(waitingRecipeSo);

                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSoList.Count; i++)
        {
            RecipeSO waitingRecipeSo = waitingRecipeSoList[i];
            if (waitingRecipeSo.kitchenObjectSoList.Count == plateKitchenObject.GetKitchenObjectSoList().Count)
            {
                //has same num of ingredients
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSo in waitingRecipeSo.kitchenObjectSoList)
                {
                    //cycling through all ingredients in the recipe
                    bool igredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
                    {
                        //cycling through all ingredients in the plate
                        if (plateKitchenObjectSo == recipeKitchenObjectSo)
                        {
                            //ingredient mathes
                            igredientFound = true;
                            break;
                        }
                    }

                    if (!igredientFound)
                    {
                        //this recipe ingredient was not found on the plate
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    //player delivered the correct recipe
                    waitingRecipeSoList.RemoveAt(i);
                    successfulRecipeDelivered++;
                    OnRecipeCompeleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSucess?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }

        //no matches found 
        //player did not delver the correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSoList()
    {
        return waitingRecipeSoList;
    }

    public int GetSuccessfulRecipeDelivered()
    {
        return successfulRecipeDelivered;
    }
}