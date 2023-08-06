using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour{

    public static OptionsUI Instance{ get; private set; }
    
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button GamepadInteractButton;
    [SerializeField] private Button GamepadInteractAltButton;
    [SerializeField] private Button GamepadPauseButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI GamepadInteractText;
    [SerializeField] private TextMeshProUGUI GamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI GamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake(){
        Instance = this;
        
        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });
        
        //Rebinding listeners
        moveUpButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        moveDownButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        moveRightButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });
        pauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Pause);
        });
        GamepadInteractButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });
        GamepadInteractAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });
        GamepadPauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });
    }

    private void Start(){
        KitchenGameManager.Instance.OnGameUnPaused += (sender, e) => {
            Hide();
        };
        
        UpdateVisual();
        
        HidePressToRebindKey();
        Hide();
    }

    private void UpdateVisual(){
        soundEffectsText.text = "Sound Effects : " + Math.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music : " + Math.Round(MusicManager.Instance.GetVolume() * 10f);
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        GamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        GamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        GamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction){
        this.onCloseButtonAction = onCloseButtonAction;
        
        gameObject.SetActive(true);
        
        soundEffectsButton.Select();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey(){
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey(){
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding){
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
