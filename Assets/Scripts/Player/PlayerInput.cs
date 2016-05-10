using UnityEngine;
using System.Collections;

/// <summary>
/// This Component captures uses input and passes it along
/// to the appropiate player component.
/// </summary>
[RequireComponent (typeof(Movement))]
public class PlayerInput : MonoBehaviour
{
    ///<summary> Component that handles the movement issued by the input captured here </summary>
    [SerializeField]
    private Movement m_PlayerMovement;
    /// <summary> Amount of horizontal axis captured</summary>
    private Vector3 rotationAmount = Vector3.zero;

	
	void FixedUpdate ()
    {
        rotationAmount.z = Input.GetAxis("Horizontal");
        if (rotationAmount != Vector3.zero)
        {
            m_PlayerMovement.Rotate(rotationAmount);
        }
	}
}
