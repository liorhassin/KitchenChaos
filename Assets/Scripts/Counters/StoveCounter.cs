using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }
    
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (!HasKitchenObject()) return;
        switch (state) {
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                });
                
                if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                    
                    burningTimer = 0f;
                    burningRecipeSO = GetBurningRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    state = State.Fried;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });
                }
                break;
            case State.Fried:
                burningTimer += Time.deltaTime;
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                });
                
                if (burningTimer > burningRecipeSO.burningTimerMax) {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                    
                    state = State.Burned;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = 0f
                    });
                }
                break;
            case State.Burned:
                break;
        }
    }


    public override void Interact(Player player) {
        if (!HasKitchenObject()) { //Counter has no object on it.
            if (!player.HasKitchenObject()) return; //Player is not holding an object.
            if (!HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO())) return; //Recipe is not found for player's object.
            player.GetKitchenObject().SetKitchenObjectParent(this); //Place object on counter.
            
            fryingRecipeSO = GetFryingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
            fryingTimer = 0f;
            
            state = State.Frying;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                state = state
            });
            
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
            });
        }
        else { //Counter has an object on it
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player is holding a plate
                    if (plateKitchenObject != null && plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                        
                        //Picked meat to plate, reset state and progress bar.
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                }
                return; //Player is already holding an object.
            }
            GetKitchenObject().SetKitchenObjectParent(player);
            
            state = State.Idle;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                state = state
            });
            
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = 0f
            });
        }
    }
    
    //Helper method to find if a recipe exists for given KitchenObjectSO
    private bool HasRecipeForInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOForInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }
    
    //Helper method to find the correct input and return the expected output(using recipe search)
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOForInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null) return fryingRecipeSO.output;
        return null;
    }
    
    //Helper method to receive the FryingRecipeSO
    private FryingRecipeSO GetFryingRecipeSOForInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
            if (fryingRecipeSO.input == inputKitchenObjectSO) return fryingRecipeSO;
        return null;
    }
    
    //Helper method to receive the FryingRecipeSO
    private BurningRecipeSO GetBurningRecipeSOForInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
            if (burningRecipeSO.input == inputKitchenObjectSO) return burningRecipeSO;
        return null;
    }
    
}
