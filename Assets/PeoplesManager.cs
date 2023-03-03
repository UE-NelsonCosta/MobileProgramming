using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeoplesManager : MonoBehaviour
{
    [SerializeField] private Person[] people;

    private void Start()
    {
        for (int i = 0; i < people.Length; i++)
        {
            Debug.Log(people[i].Name);
        }
    }
}
