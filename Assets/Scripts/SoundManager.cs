using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour{

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    
    private void Start(){
        DeliveryManager.Instance.OnRecipeSuccess += (sender, e) => {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
        };
        DeliveryManager.Instance.OnRecipeFailed += (sender, e) => {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
        };
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f){
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f){
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

}


