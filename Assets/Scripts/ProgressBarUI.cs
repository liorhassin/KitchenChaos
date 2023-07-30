using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour{

    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start(){
        cuttingCounter.OnProgressChanged += (sender, e) => {
            barImage.fillAmount = e.progressNormalized;
            if(barImage.fillAmount == 0f || barImage.fillAmount == 1f) Hide();
            else Show();
        };
        barImage.fillAmount = 0f;
        Hide();
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
