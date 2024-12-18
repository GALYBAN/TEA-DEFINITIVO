using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayerController : MonoBehaviour
{
    private PlayerInputs inputs;
    private MovementController movementController;
    private PlayerGravity gravityController;
    private MaskingController maskingController;
    private PhotoTriggerController photoTriggerController;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PhysicMaterial lowFrictionMaterial;
    [SerializeField] private PhysicMaterial defaultMaterial;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (defaultMaterial != null)
        {
            characterController.material = defaultMaterial;
        }
        inputs = GetComponent<PlayerInputs>();
        movementController = GetComponent<MovementController>();
        gravityController = GetComponent<PlayerGravity>();
        maskingController = GetComponent<MaskingController>();
        photoTriggerController = GetComponent<PhotoTriggerController>();
    }

    void Start(){
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        if (inputs.JumpPressed && !maskingController.isColorfull && !photoTriggerController.inCoroutine)
        {
            gravityController.Jump();

            if (inputs.HorizontalInput < 0)
            {
                movementController.SetLastKey("left");
            }
            else if (inputs.HorizontalInput > 0)
            {
                movementController.SetLastKey("right");
            }
        }

        if(inputs.PhotoPressed && photoTriggerController.isFocus) photoTriggerController.TakePhoto(photoTriggerController.photoSpot);

        // Detectar colisiones con paredes
        if ((characterController.collisionFlags & CollisionFlags.Sides) != 0)
        {
            // Cambiar el material a uno con menor fricción
            if (lowFrictionMaterial != null)
            {
                characterController.material = lowFrictionMaterial;
            }
        }
        else
        {
            // Restaurar el material por defecto
            if (defaultMaterial != null) characterController.material = defaultMaterial;
            
        }

        maskingController.Masking();
        if(!maskingController.isColorfull && !photoTriggerController.inCoroutine)movementController.Move();
        gravityController.ApplyGravity(GetComponent<CharacterController>());
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Cambiar la fricción según la superficie o el material
        if (hit.collider.CompareTag("SlipperySurface"))
        {
            // Ajusta el comportamiento de fricción si estás en una superficie resbaladiza
            characterController.material.dynamicFriction = 0.2f;
            characterController.material.staticFriction = 0.2f;
        }
        else
        {
            // Físicas por defecto
            characterController.material.dynamicFriction = 0.6f;
            characterController.material.staticFriction = 0.6f;
        }
    }

}
