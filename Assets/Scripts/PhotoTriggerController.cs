using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTriggerController : MonoBehaviour
{
    [SerializeField] GameObject[] _photoTriggers;
    [SerializeField] public bool isFocus;
    private MaskingController maskingController;
    private PlayerInputs inputs;
    public string photoSpot;
    public Camera mainCamera;
    public Camera[] cameras;
    bool inCoroutine=false;

    void Awake(){
        maskingController=GetComponent<MaskingController>();
        inputs = GetComponent<PlayerInputs>();
    }

    void Start(){
        mainCamera.gameObject.SetActive(true);
        foreach (var camera in cameras){
            camera.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collider){
        if(collider.transform.parent.CompareTag("PhotoTrigger")){
            maskingController.isColorfull=true;
            maskingController.stopTimer=true;
            isFocus=true;
            photoSpot=collider.tag;
        }
    }
    void OnTriggerExit(Collider collider){
        if(collider.transform.parent.CompareTag("PhotoTrigger")){
            maskingController.isColorfull=false;
            maskingController.stopTimer=false;
            maskingController.maskingTimer=0;
            isFocus=false;
        }
    }

    public void TakePhoto(string photoSpot){
        switch (photoSpot){
            case "Photo1":
                Debug.Log("Foto 1");
                if(!inCoroutine) StartCoroutine(SwitchCamera(0));
            break;
            case "Photo2":
                Debug.Log("Foto 2");
            break;
            case "Photo3":
                Debug.Log("Foto 3");
            break;
        }
    }

    private IEnumerator SwitchCamera(int camIndex){
        inCoroutine=true;
        mainCamera.gameObject.SetActive(false);
        if (camIndex >= 0 && camIndex < cameras.Length) cameras[camIndex].gameObject.SetActive(true);  

        yield return new WaitForSeconds(4f);

        if (camIndex >= 0 && camIndex < cameras.Length) cameras[camIndex].gameObject.SetActive(false);  
        mainCamera.gameObject.SetActive(true);
        inCoroutine=false;
    }
}