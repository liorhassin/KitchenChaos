using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour{

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    
    public static SoundManager Instance{ get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = .5f;

    private void Awake(){
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, .7f);
    }

    private void Start(){
        DeliveryManager.Instance.OnRecipeSuccess += (sender, e) => {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
        };
        DeliveryManager.Instance.OnRecipeFailed += (sender, e) => {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
        };
        CuttingCounter.OnAnyCut += (sender, e) => {
            CuttingCounter cuttingCounter = sender as CuttingCounter;
            PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
        };
        Player.Instance.OnPickedSomething += (sender, e) => {
            Player player = sender as Player;
            PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
        };
        BaseCounter.OnAnyObjectPlacedHere += (sender, e) => {
            BaseCounter baseCounter = sender as BaseCounter;
            PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
        };
        TrashCounter.OnAnyObjectTrashed += (sender, e) => {
            TrashCounter trashCounter = sender as TrashCounter;
            PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
        };
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f){
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f){
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volumeMultiplier){
        PlaySound(audioClipRefsSO.footstep, position, volumeMultiplier * volume);
    }

    public void ChangeVolume(){
        volume += .1f;
        if (volume >= 1f) volume = 0f;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume(){
        return volume;
    }
    
}


