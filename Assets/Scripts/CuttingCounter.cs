using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter{

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    
    public override void Interact(Player player) {
        
        if (!HasKitchenObject()) { //Counter has no object on it.
            if (!player.HasKitchenObject()) return; //Player is not holding an object.
            if (!HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO())) return; //Recipe is not found for player's object.
            player.GetKitchenObject().SetKitchenObjectParent(this); //Place object on counter.
        }
        else { //Counter has an object on it
            if (player.HasKitchenObject()) return; //Player is already holding an object.
            GetKitchenObject().SetKitchenObjectParent(player); //TEMP - Take object like in clear counter
        }
    }

    public override void InteractAlternate(Player player){
        if (!HasKitchenObject() || !HasRecipeForInput(GetKitchenObject().GetKitchenObjectSO())) return;
        KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
    }
    
    
    
    //Helper method to find if a recipe exists for given KitchenObjectSO
    private bool HasRecipeForInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
            if (cuttingRecipeSO.input == inputKitchenObjectSO) return true;
        return false;
    }
    
    //Helper method to find the correct input and return the expected output(using recipe search)
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
            if (cuttingRecipeSO.input == inputKitchenObjectSO) return cuttingRecipeSO.output;
            return null;
    }
}
