using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    
    private float spawnPlateTimer;
    private const float SpawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private const int PlatesSpawnedAmountMax = 4;

    private void Start(){
        spawnPlateTimer = 3f; //Make first plate when starting the game come out faster.
    }

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer < SpawnPlateTimerMax) return;
        spawnPlateTimer = 0f;
        if (platesSpawnedAmount >= PlatesSpawnedAmountMax) return;
        if (!KitchenGameManager.Instance.IsGamePlaying()) return; //Game didn't start yet, Player is reading the tutorial.
        
        //All requirements are met, Plate can be spawned.
        platesSpawnedAmount++;
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject() || platesSpawnedAmount == 0) return;
        platesSpawnedAmount--;
        KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }
}
