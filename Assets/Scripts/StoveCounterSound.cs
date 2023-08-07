using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour{

    [SerializeField] private StoveCounter stoveCounter;
    
    private AudioSource audioSource;
    private float warningSoundTimer;
    private const float warningSoundTimerMax = .2f;
    private bool playWarningSound;
    
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void Start(){
        stoveCounter.OnStateChanged += (sender, e) => {
            bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
            if (playSound) audioSource.Play();
            else audioSource.Pause();
        };
        stoveCounter.OnProgressChanged += (sender, e) => {
            float burnShowProgressAmount = .5f;
            playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        };
    }

    private void Update(){
        if (!playWarningSound) return;
        warningSoundTimer -= Time.deltaTime;
        if (warningSoundTimer > 0f) return;
        warningSoundTimer = warningSoundTimerMax;
        SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
    }
}
