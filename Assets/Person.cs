using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    
    [SerializeField] private string name = "";
    [SerializeField] private int age = 0;

    // Property Accessors
    public string Name {
        get { return name; }
    }
    public string Age { get; }
}
