using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start(){
        KitchenGameManager.Instance.OnStateChanged += (sender, e) => {
            if (KitchenGameManager.Instance.IsGameOver()) {
                recipesDeliveredText.text = DeliveryManager.Instance.GetRecipesDeliveredAmount().ToString();
                Show();
            }
            else Hide();
        };

        Hide();
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
    
}
