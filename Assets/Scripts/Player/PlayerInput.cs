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
    private Movement _playerMovement;
    /// <summary> Amount of horizontal axis captured</summary>
    private Vector3 _rotationAmount = Vector3.zero;

	
	void FixedUpdate ()
    {
        _rotationAmount.z = Input.GetAxis("Horizontal");
        _playerMovement.Rotate(_rotationAmount);
        /*
        if (_rotationAmount != Vector3.zero)
        {
            _playerMovement.Rotate(_rotationAmount);
        }
        */
	}
}
