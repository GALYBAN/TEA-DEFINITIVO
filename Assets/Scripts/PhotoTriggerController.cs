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
    bool ph2 = false;
    bool ph3 = false;

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

    void OnTriggerStay(Collider collider){
        if(collider.transform.parent.CompareTag("PhotoTrigger")) maskingController.SetColorFull(0);
    }

    void OnTriggerEnter(Collider collider){
    if (collider.transform.parent.CompareTag("PhotoTrigger")) {
        maskingController.isColorfull = true;
        maskingController.stopTimer = true;
        isFocus = true;
        photoSpot = collider.tag; // Aquí se asigna el tag del trigger actual
    }
}

void OnTriggerExit(Collider collider){
    if (collider.transform.parent.CompareTag("PhotoTrigger")) {
        maskingController.isColorfull = false;
        maskingController.stopTimer = false;
        maskingController.maskingTimer = 0;
        isFocus = false;
        photoSpot = ""; // Reiniciamos photoSpot al salir del trigger
        maskingController.SetColorFull(1);
    }
}

public void TakePhoto(string photoSpot){
    if (inCoroutine) return; // Evitar múltiples fotos a la vez

    switch (photoSpot) {
        case "Photo1":
            if (!ph1) {
                Debug.Log("Foto 1");
                StartCoroutine(SwitchCamera(0));
                ph1 = true; // Marcar como tomada
            } else {
                Debug.Log("Foto 1 ya fue tomada");
            }
            break;
        case "Photo2":
            if (!ph2) {
                Debug.Log("Foto 2");
                StartCoroutine(SwitchCamera(1));
                ph2 = true;
            } else {
                Debug.Log("Foto 2 ya fue tomada");
            }
            break;
        case "Photo3":
            if (!ph3) {
                Debug.Log("Foto 3");
                StartCoroutine(SwitchCamera(2));
                ph3 = true;
            } else {
                Debug.Log("Foto 3 ya fue tomada");
            }
            break;
        default:
            Debug.Log("No estás en un PhotoSpot válido");
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