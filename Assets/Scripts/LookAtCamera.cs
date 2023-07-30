using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LookAtCamera : MonoBehaviour{
    
    private void LateUpdate(){
        transform.LookAt(Camera.main.transform);
    }
}
