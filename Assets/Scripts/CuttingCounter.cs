using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter{

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    
    public override void Interact(Player player) {
        
        if (!HasKitchenObject()) { //Counter has no object on it.
            if (!player.HasKitchenObject()) return; //Player is not holding an object.
            player.GetKitchenObject().SetKitchenObjectParent(this); //Place object on counter.
        }
        else { //Counter has an object on it
            if (player.HasKitchenObject()) return; //Player is already holding an object.
            GetKitchenObject().SetKitchenObjectParent(player); //TEMP - Take object like in clear counter
        }
    }

    public override void InteractAlternate(Player player){
        if (!HasKitchenObject()) return;
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
    }
}
