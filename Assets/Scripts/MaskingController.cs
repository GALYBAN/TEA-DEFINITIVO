using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskingController : MonoBehaviour
{
    [SerializeField] public bool isColorfull;
    public float maskingTimer;
    [SerializeField] float maskingTime = 5f;
    [SerializeField] float unmaskingTimer;
    [SerializeField] float unmaskingWaitTime = 2f;
    public bool stopTimer = false;



    [SerializeField] Material _material;

    [SerializeField] Image _maskingEffect;
    private Transform _cameraTransform;
    private Vector3 _originalCameraPosition;

    [Header("Camera Shake Settings")]
    [SerializeField] bool enableCameraShake = true;
    [SerializeField] float maxShakeIntensity = 0.2f; // Máxima intensidad del temblor.
    [SerializeField] float shakeFrequency = 20f;

    private float currentShakeIntensity = 0f; // Intensidad dinámica del temblor.
    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _originalCameraPosition = _cameraTransform.position;
    }

    private void Update()
    {
        Masking();

        if (enableCameraShake)
        {
            if (isColorfull)
            {
                // Incrementar progresivamente la intensidad del temblor durante el masking.
                currentShakeIntensity = Mathf.Lerp(0f, maxShakeIntensity, maskingTimer / maskingTime);
                CameraShake();
            }
            else
            {
                // Detener el temblor de golpe durante el unmasking.
                currentShakeIntensity = 0f;
                _cameraTransform.position = _originalCameraPosition;
            }
        }
    }

    public void Masking()
    {
        maskingTimer += Time.deltaTime;

        if (maskingTimer >= maskingTime && !isColorfull && !stopTimer){
            isColorfull = true;
            unmaskingTimer = 0f;
            _material.SetFloat("_IsWhite", 0f);
        }

        if (isColorfull){
            unmaskingTimer += Time.deltaTime;
            if (unmaskingTimer >= unmaskingWaitTime){
                isColorfull = false;
                maskingTimer = 0f;
                _material.SetFloat("_IsWhite", 1f);
            }
        }

        // Ajustar la opacidad del maskingEffect
        if (!isColorfull && !stopTimer)
        {
            // Aumentar la opacidad progresivamente durante el masking.
            float alpha = Mathf.Lerp(0f, 1f, maskingTimer / maskingTime);
            _maskingEffect.color = new Color(_maskingEffect.color.r, _maskingEffect.color.g, _maskingEffect.color.b, alpha);
        }
        else
        {
            // Disminuir la opacidad progresivamente durante el unmasking.
            float alpha = Mathf.Lerp(1f, 0f, unmaskingTimer / unmaskingWaitTime);
            _maskingEffect.color = new Color(_maskingEffect.color.r, _maskingEffect.color.g, _maskingEffect.color.b, alpha);
        }

    }
    void CameraShake()
    {
        float offsetX = Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) * currentShakeIntensity - (currentShakeIntensity / 2f);
        float offsetY = Mathf.PerlinNoise(0f, Time.time * shakeFrequency) * currentShakeIntensity - (currentShakeIntensity / 2f);

        _cameraTransform.position = _originalCameraPosition + new Vector3(offsetX, offsetY, 0f);
    }
}

