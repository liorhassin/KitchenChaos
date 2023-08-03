using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {
    public override void Interact(Player player){
        if (!player.HasKitchenObject()) return;
        player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject);
        if (plateKitchenObject == null) return;
        //Reached here, Player is holding an object and that object is a Plate.
        
        player.GetKitchenObject().DestroySelf();
    }
}
