using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskingController : MonoBehaviour
{
    [SerializeField] public bool isColorfull;
    [SerializeField] public float maskingTimer;
    [SerializeField] float maskingTime=5f;
    [SerializeField] float unmaskingTimer;
    [SerializeField] float unmaskingWaitTime=2f;

    public bool stopTimer = false;
    
    public void Masking(){
        maskingTimer+=Time.deltaTime;
        if(maskingTimer>=maskingTime && !isColorfull && !stopTimer) {
            isColorfull=true;
            unmaskingTimer=0f;    
        }
    
        if(isColorfull){
            unmaskingTimer+=Time.deltaTime;
            if(unmaskingTimer>=unmaskingWaitTime){
                isColorfull=false;
                maskingTimer=0f;
            }
        }
    }
}
