using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject m_ObstaclePrefab;
    public float m_TimeToSpawn = 5f;
    private float timeElapsed = 0f;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeElapsed += Time.deltaTime;
        print(timeElapsed);
        if (timeElapsed  >= m_TimeToSpawn)
        {
            print("Spawned");
            timeElapsed -= m_TimeToSpawn;
            Instantiate(m_ObstaclePrefab, transform.position, transform.rotation);
        }
	}
}
