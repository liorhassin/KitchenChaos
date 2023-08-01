using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) { //Counter has no object on it.
            if (!player.HasKitchenObject()) return; //Player is not holding an object.
            player.GetKitchenObject().SetKitchenObjectParent(this);
        }
        else { //Counter has an object on it
            if (player.HasKitchenObject()){
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player is holding a plate
                    if (plateKitchenObject != null && plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else {
                    //Player is not carrying a Plate but something else.
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //Counter has a plate on top of it.
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
                return; //Player is already holding an object.
            }
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}
