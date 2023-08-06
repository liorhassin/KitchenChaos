using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI keyMoveUp;
    [SerializeField] private TextMeshProUGUI keyMoveLeft;
    [SerializeField] private TextMeshProUGUI keyMoveDown;
    [SerializeField] private TextMeshProUGUI keyMoveRight;
    [SerializeField] private TextMeshProUGUI keyInteract;
    [SerializeField] private TextMeshProUGUI keyInteractAlternate;
    [SerializeField] private TextMeshProUGUI keyPause;
    [SerializeField] private TextMeshProUGUI keyGamepadInteract;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractAlternate;
    [SerializeField] private TextMeshProUGUI keyGamepadPause;

    private void Start(){
        GameInput.Instance.OnBindingRebind += (sender, e) => {
            UpdateVisual();
        };
        KitchenGameManager.Instance.OnStateChanged += (sender, e) => {
            if (KitchenGameManager.Instance.IsCountdownToStartActive()) {
                Hide();   
            }
        };
        UpdateVisual();
        Show();
    }

    private void UpdateVisual(){
        keyMoveUp.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveLeft.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveDown.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveRight.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlternate.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        keyPause.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        keyGamepadInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        keyGamepadInteractAlternate.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        keyGamepadPause.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
