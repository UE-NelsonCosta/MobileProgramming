using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;

    [SerializeField] private Vector3 positionalOffset;
    
    private void Update()
    {
        transform.position = objectToFollow.position + positionalOffset;
    }
}
