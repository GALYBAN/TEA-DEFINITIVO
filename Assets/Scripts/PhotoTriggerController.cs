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
    public bool inCoroutine=false;

    bool ph1 = false;
    bool ph2;
    bool ph3;

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
    void OnTriggerStay(Collider collider){
        if(collider.transform.parent.CompareTag("PhotoTrigger")) maskingController.SetColorFull(0);
    }
    void OnTriggerExit(Collider collider){
        if(collider.transform.parent.CompareTag("PhotoTrigger")){
            maskingController.isColorfull=false;
            maskingController.stopTimer=false;
            maskingController.maskingTimer=0;
            isFocus=false;
            maskingController.SetColorFull(1);
        }
    }

    public void TakePhoto(string photoSpot){
        switch (photoSpot){
            case "Photo1":
                Debug.Log("Foto 1");
                if(!inCoroutine && !ph1) StartCoroutine(SwitchCamera(0));
                ph1=true;
            break;
            case "Photo2":
                Debug.Log("Foto 2");
            break;
            case "Photo3":
                Debug.Log("Foto 3");
            break;
        }
    }

    private IEnumerator SwitchCamera(int camIndex){ //TODO cuando este haciendo la foto no puede moverse el personaje
        inCoroutine=true;
        mainCamera.gameObject.SetActive(false);
        if (camIndex >= 0 && camIndex < cameras.Length) cameras[camIndex].gameObject.SetActive(true);  

        yield return new WaitForSeconds(4f);

        if (camIndex >= 0 && camIndex < cameras.Length) cameras[camIndex].gameObject.SetActive(false);  
        mainCamera.gameObject.SetActive(true);
        inCoroutine=false;
    }
}
