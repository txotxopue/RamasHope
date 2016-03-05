using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public List<GameObject> ObstaclePool;

    void Awake()
    {
        ObstaclePool = new List<GameObject>();
    }

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
        if (timeElapsed >= m_TimeToSpawn)
        {
            //print("Spawned");
            timeElapsed -= m_TimeToSpawn;
            SpawnObstacle();
        }
	}

    private void SpawnObstacle ()
    {
        /*
        randomRadialPosition = Random.Range(lastRadialPosition - m_radialSpread, lastRadialPosition + m_radialSpread);
        rotationVector.z = GetRotation(randomRadialPosition);
        Instantiate(m_ObstaclePrefab, transform.position, Quaternion.Euler(rotationVector));
        lastRadialPosition = randomRadialPosition;
        */

        GameObject newObstacle = GetObstacleFromPool();
        newObstacle.transform.position = transform.position;
        randomRadialPosition = Random.Range(lastRadialPosition - m_radialSpread, lastRadialPosition + m_radialSpread);
        rotationVector.z = GetRotation(randomRadialPosition);
        newObstacle.transform.rotation = Quaternion.Euler(rotationVector);
        newObstacle.SetActive(true);
        newObstacle.SendMessage("InitObstacle");
        newObstacle.transform.SetParent(this.transform);
        lastRadialPosition = randomRadialPosition;
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


    /**
    * Gets an obstacle from the pool.
    * If there is no inactive obstacle on the pool,
    * it creates a new obstacle.
    */
    public GameObject GetObstacleFromPool ()
    {
        for (int i = 0; i < ObstaclePool.Count -1; i++)
        {
            if (!ObstaclePool[i].activeInHierarchy)
            {
                return ObstaclePool[i];
            }
        }
        GameObject newObstacle = (GameObject)Instantiate(m_ObstaclePrefab);
        ObstaclePool.Add(newObstacle);
        return newObstacle;
    }
}
