using UnityEngine;
using System.Collections;

public class WallMover : MonoBehaviour
{
    public float m_MovementSpeed = 50f;
    private Vector3 movement = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        movement.z = m_MovementSpeed * Time.deltaTime;
        transform.Translate(movement);
	}
}
