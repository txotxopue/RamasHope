using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    public float m_MovementSpeed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(m_MovementSpeed * Mathf.Sin(Time.realtimeSinceStartup) * Time.deltaTime * Vector3.forward);
	}
}
