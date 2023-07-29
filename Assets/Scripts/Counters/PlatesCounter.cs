using System;
using DefaultNamespace;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer = 0;
    private const float spawnPlateTimerMax = 4f;
    private int plateSpawnAmount;
    private const int plateSpawnedAmountMax = 4;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (GameManager.Instance.IsGamePlaying() && plateSpawnAmount < plateSpawnedAmountMax)
            {
                plateSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is empty 
            if (plateSpawnAmount > 0)
            {
                //there is a plate 
                plateSpawnAmount--;

                KitchenObjects.SpawnKithcenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}