using UnityEngine;
using System.Collections;

public class WallMover : MonoBehaviour
{
    public float m_MovementSpeed = 100f;
    private Vector3 movement = Vector3.zero;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        movement.z = GameInstance.GetCurrentGameManager().GetTubeSpeed() * Time.deltaTime;  
        transform.Translate(movement);
	}
}
