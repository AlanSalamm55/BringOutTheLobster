using System;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnstateChangeEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnprogressChangeEventArgs> OnProgressChange;

    public class OnstateChangeEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    private State state;
    [SerializeField] private FryingRecipeSO[] fryingRecipeSoArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSoArray;

    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSo;
    private BurningRecipeSO burningRecipeSo;


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    // Handle idle state logic
                    break;

                case State.Frying:

                    fryingTimer += Time.deltaTime;

                    OnProgressChange?.Invoke(this,
                        new IHasProgress.OnprogressChangeEventArgs
                            { progressNormalized = fryingTimer / fryingRecipeSo.fryingTimerMax });

                    if (fryingTimer >= fryingRecipeSo.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObjects.SpawnKithcenObject(fryingRecipeSo.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSo = GetBurningRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
                        OnStateChanged?.Invoke(this, new OnstateChangeEventArgs { state = state });
                    }

                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChange?.Invoke(this,
                        new IHasProgress.OnprogressChangeEventArgs
                            { progressNormalized = burningTimer / burningRecipeSo.burningTimerMax });

                    if (burningTimer >= burningRecipeSo.burningTimerMax)
                    {
                        burningTimer = 0;
                        GetKitchenObject().DestroySelf();
                        KitchenObjects.SpawnKithcenObject(burningRecipeSo.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnstateChangeEventArgs { state = state });
                    }

                    break;

                case State.Burned:
                    OnProgressChange?.Invoke(this,
                        new IHasProgress.OnprogressChangeEventArgs
                            { progressNormalized = 0f });
                    break;
            }
        }
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
                    fryingRecipeSo = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnstateChangeEventArgs { state = state });
                    OnProgressChange?.Invoke(this,
                        new IHasProgress.OnprogressChangeEventArgs
                            { progressNormalized = fryingTimer / fryingRecipeSo.fryingTimerMax });
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
                    state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnstateChangeEventArgs { state = state });
                    OnProgressChange?.Invoke(this,
                        new IHasProgress.OnprogressChangeEventArgs
                            { progressNormalized = 0f });
                }
            }
            else
            {
                //player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnstateChangeEventArgs { state = state });
                OnProgressChange?.Invoke(this,
                    new IHasProgress.OnprogressChangeEventArgs
                        { progressNormalized = 0f });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
        return fryingRecipeSo != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryingRecipeSO fryingRecipeSo in fryingRecipeSoArray)
        {
            if (fryingRecipeSo.input == inputKitchenObjectSo)
            {
                return fryingRecipeSo;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (BurningRecipeSO burningRecipeSo in burningRecipeSoArray)
        {
            if (burningRecipeSo.input == inputKitchenObjectSo)
            {
                return burningRecipeSo;
            }
        }

        return null;
    }

    public bool IsFried()
    {
        return state == State.Fried;
    }
    
}