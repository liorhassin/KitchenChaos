using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour{

    public static DeliveryManager Instance{ get; private set; }


    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    

    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private const float SpawnRecipeTimerMax = 4f;
    private const int WaitingRecipeMaxSize = 4;

    private void Awake(){
        Instance = this;
        
        waitingRecipeSOList = new List<RecipeSO>();
        spawnRecipeTimer = SpawnRecipeTimerMax;
    }

    private void Start(){
        spawnRecipeTimer = 1f; //Make first recipe come faster when starting the game.
    }

    private void Update(){
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer > 0f) return; //Timer didn't reach 0 yet.
        spawnRecipeTimer = SpawnRecipeTimerMax; //Reset timer
        if (waitingRecipeSOList.Count >= WaitingRecipeMaxSize) return; //Reached maximum amount of recipes allowed.
        
        //Recipe timer requirement is met and there is still a spot for new recipe, New recipe can be spawned.
        waitingRecipeSOList.Add(recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)]);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
        for (int i = 0; i < recipeListSO.recipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count != plateKitchenObject.GetKitchenObjectSOList().Count) continue;
            
            bool plateContentMatchesRecipe = true;
            foreach (KitchenObjectSO kitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                //Cycle through all the ingredients in current recipe
                bool foundIngredient = false;
                foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                    //Cycle through all the ingredients in current plate
                    if (plateKitchenObjectSO == kitchenObjectSO) {
                        foundIngredient = true;
                        break;
                    }
                }

                if (!foundIngredient) {
                    plateContentMatchesRecipe = false;
                    break;
                }
            }

            if (plateContentMatchesRecipe) {
                //Player delivered a valid recipe
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                
                return;
            }
        }
    }


    public List<RecipeSO> GetWaitingRecipeSOList(){
        return waitingRecipeSOList;
    }
}
