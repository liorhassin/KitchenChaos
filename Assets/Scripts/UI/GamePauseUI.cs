using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour{
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake(){
        resumeButton.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }

    private void Start(){
        KitchenGameManager.Instance.OnGamePaused += (sender, e) => {
            Show();
        };
        KitchenGameManager.Instance.OnGameUnPaused += (sender, e) => {
            Hide();
        };

        Hide();
    }

    private void Show(){
        gameObject.SetActive(true);
        
        resumeButton.Select();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

}
