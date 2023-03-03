using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndgameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Do Some Engame Stuff Here Like A UI Or Reload A Scene
            Debug.Log("Endgame Triggered!");
        }
        
        Debug.Log(other.gameObject);
    }
    
    private void OnTriggerExit(Collider other)
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);

        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(r, g, b, 1.0f));
    }

    private void OnCollisionExit(Collision other)
    {
        throw new NotImplementedException();
    }
}
