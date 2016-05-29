using UnityEngine;
using System.Collections;

/// <summary>
/// Moves the elements in the tube.
/// In order to simulate that the player's ship is moving,
/// we actually move the objects in the tube with this script.
/// </summary>
public class WallMover : MonoBehaviour
{
    /// <summary>Movement vector, initialized to a zero vector.</summary>
    private Vector3 movement = Vector3.zero;
	
	
	void FixedUpdate ()
    {
        // Z is the axis along the tube
        movement.z = GameInstance.GetCurrentGameManager().GetTubeSpeed() * Time.deltaTime;  
        transform.Translate(movement);
	}
}
