using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) return; //Player is already holding a kitchen object.
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
