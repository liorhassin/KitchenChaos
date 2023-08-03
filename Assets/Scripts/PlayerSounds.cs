using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour{
    private Player player;

    private float footstepTimer;
    private const float FootstepTimerMax = .1f;
    
    private void Awake(){
        player = GetComponent<Player>();
    }

    private void Update(){
        footstepTimer -= Time.deltaTime;
        if (footstepTimer > 0f) return; //Didn't reach timer for footstep sound
        footstepTimer = FootstepTimerMax;
        if (!player.IsWalking()) return; //Player is not walking, No sound is needed.
        float volume = 1f;
        SoundManager.Instance.PlayFootstepsSound(player.transform.position, 1f);
    }
}
