using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaycastOnTerrainToMoveAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private float rayDistance = 100.0f;

    private void Update()
    {
        Vector3 rayInitialPosition = transform.position;
        Vector3 rayForwards = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, rayDistance));

        Debug.DrawLine(rayInitialPosition, rayInitialPosition + rayForwards);
        
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = new Ray(rayInitialPosition, rayForwards);
            RaycastHit hitInformation;
            if (Physics.Raycast(ray,  out hitInformation, rayDistance))
            {
                agent.SetDestination(hitInformation.point);
            }
        }
    }

    public void OnTapOccured(Vector2 tapPosition)
    {
        Vector3 rayInitialPosition = transform.position;
        Vector3 rayForwards = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, rayDistance));

        Ray ray = new Ray(rayInitialPosition, rayForwards);
        RaycastHit hitInformation;
        if (Physics.Raycast(ray, out hitInformation, rayDistance))
        {
            agent.SetDestination(hitInformation.point);
        }
    }
}
