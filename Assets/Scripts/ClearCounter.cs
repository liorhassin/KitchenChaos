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
            if (player.HasKitchenObject()) return; //Player is already holding an object.
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}
