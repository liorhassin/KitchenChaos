using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    
    public override void Interact(Player player) {
        
        if (!HasKitchenObject()) { //Counter has no object on it.
            if (!player.HasKitchenObject()) return; //Player is not holding an object.
            if (!HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO())) return; //Recipe is not found for player's object.
            player.GetKitchenObject().SetKitchenObjectParent(this); //Place object on counter.
            
            cuttingProgress = 0;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
        }
        else { //Counter has an object on it
            if (player.HasKitchenObject()) return; //Player is already holding an object.
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }

    public override void InteractAlternate(Player player){
        if (!HasKitchenObject() || !HasRecipeForInput(GetKitchenObject().GetKitchenObjectSO())) return;
        
        //Handle events(Cut and Progress change)
        OnCut?.Invoke(this, EventArgs.Empty);
        cuttingProgress++;
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
            progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
        });
        //Stop handling events

        if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){ //Reached max progress bar item should be sliced.
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }
    
    
    
    //Helper method to find if a recipe exists for given KitchenObjectSO
    private bool HasRecipeForInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    
    //Helper method to find the correct input and return the expected output(using recipe search)
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null) return cuttingRecipeSO.output;
        return null;
    }
    
    //Helper method to receive the CuttingRecipeSO
    private CuttingRecipeSO GetCuttingRecipeSOForInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
            if (cuttingRecipeSO.input == inputKitchenObjectSO) return cuttingRecipeSO;
        return null;
    }
}
