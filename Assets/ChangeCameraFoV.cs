using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraFoV : MonoBehaviour
{
    [SerializeField] private float minFOV;
    [SerializeField] private float maxFOV;

    private Camera mainCameraReference = null;

    private void Start()
    {
        mainCameraReference = Camera.main;
    }

    public void OnPinchDeltaChanged(float amount)
    {
        mainCameraReference.fieldOfView = 
            Mathf.Clamp(mainCameraReference.fieldOfView + (amount * Time.deltaTime * multiplier), minFOV, maxFOV);
    }
}
