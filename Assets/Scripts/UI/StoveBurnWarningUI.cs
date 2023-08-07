using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour{

    [SerializeField] private StoveCounter stoveCounter;

    private void Start(){
        stoveCounter.OnProgressChanged += (sender, e) => {
            float burnShowProgressAmount = .5f;
            bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
            gameObject.SetActive(show);
        };
        
        gameObject.SetActive(false);
    }
}
