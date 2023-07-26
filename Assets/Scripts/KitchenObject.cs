using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter) {
        
        //Clear KitchenObject from old counter.
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }
        
        //Add KitchenObject to new counter.
        this.clearCounter = clearCounter;
        
        if (clearCounter.HasKitchenObject()) {
            Debug.LogError("Counter already has a KitchenObject!");
        }
        
        clearCounter.SetKitchenObject(this);
        
        //Update visuals.
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }

}
