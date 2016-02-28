using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Movement))]
public class PlayerInput : MonoBehaviour
{
    private Movement m_PlayerMovement;
    private Vector3 rotationAmount = Vector3.zero;
    private Vector3 movementAmount = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        m_PlayerMovement = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rotationAmount.z = Input.GetAxis("Horizontal");
        movementAmount.z = Input.GetAxis("Movement");
        if (rotationAmount != Vector3.zero)
        {
            m_PlayerMovement.Rotate(rotationAmount);
        }
        /* Disabled!
        if (movementAmount != Vector3.zero)
        {
            m_PlayerMovement.Move(movementAmount);
        }
        */
	}
}
