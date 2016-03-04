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

    public int m_radialSpread = 3;
    private int randomRadialPosition = 0;
    private int lastRadialPosition = 0;

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

            randomRadialPosition = Random.Range(lastRadialPosition - m_radialSpread, lastRadialPosition + m_radialSpread);
            rotationVector.z = GetRotation(randomRadialPosition);
            Instantiate(m_ObstaclePrefab, transform.position, Quaternion.Euler(rotationVector));
            lastRadialPosition = randomRadialPosition;
            /*
            randomRotation = Random.Range(lastRotation - m_rotationSpread, lastRotation + m_rotationSpread);
            rotationVector.z = randomRotation;
            Instantiate(m_ObstaclePrefab, transform.position, Quaternion.Euler(rotationVector));
            lastRotation = randomRotation;
            */
        }
	}

    /**
    * Blocks can spawn in one of 24 circular position.
    * Given a number from 0 to 23, this function returns the equivalent euler angle
    * needed to spawn the prefab
    **/
    private float GetRotation(int pPosition)
    {
        return (360f / 24f * pPosition);
    }
}
