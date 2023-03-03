using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterPhysicsMover : MonoBehaviour
{
    [SerializeField] private float horizontalVelocity = 1.0f;

    [SerializeField] private float jumpForce = 10.0f;

    private Rigidbody myRigidbody = null;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, myRigidbody.velocity.y, 0);
        myRigidbody.velocity = direction * horizontalVelocity;

        if (Input.GetKeyUp(KeyCode.W))
        {
            myRigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
}
