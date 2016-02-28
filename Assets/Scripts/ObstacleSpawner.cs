using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject m_ObstaclePrefab;
    public float m_TimeToSpawn = 2f;
    public float m_rotationSpread = 60f;
    private float timeElapsed = 0f;
    private Vector3 rotationVector;
    private float randomRotation;
    private float lastRotation;

	// Use this for initialization
	void Start ()
    {
        timeElapsed = m_TimeToSpawn;
        rotationVector = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed  >= m_TimeToSpawn)
        {
            print("Spawned");
            timeElapsed -= m_TimeToSpawn;
            randomRotation = Random.Range(lastRotation - m_rotationSpread, lastRotation + m_rotationSpread);
            rotationVector.z = randomRotation;
            Instantiate(m_ObstaclePrefab, transform.position, Quaternion.Euler(rotationVector));
            lastRotation = randomRotation;
        }
	}
}
