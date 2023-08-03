using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour{

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake(){
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start(){
        //This time using Lambda expression to define the action that happens when the event is triggered.
        DeliveryManager.Instance.OnRecipeSpawned += (sender, e) => {
            UpdateVisual();
        };

        DeliveryManager.Instance.OnRecipeCompleted += (sender, e) => {
            UpdateVisual();
        };
        
        UpdateVisual();
    }

    private void UpdateVisual(){
        foreach (Transform child in container) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        List<RecipeSO> waitingRecipeSOList = DeliveryManager.Instance.GetWaitingRecipeSOList();
        foreach (RecipeSO recipeSO in waitingRecipeSOList) {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
