using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{

    public ObstacleType[] m_ObstaclePrefabs;
    public GameObject m_ObstaclePrefab;
    public float m_TimeToSpawn = 2f;
    private float timeElapsed = 0f;
    private Vector3 rotationVector;
    private float randomRotation;
    private float lastRotation;

    public int m_radialSpread = 3;
    private bool _lastAllowsOffset = false;
    //private int _randomRadialPosition = 0;
    private int _lastRadialPosition = 0;

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

        ObstacleType newObstacleType = GetRandomObstacleType();

        GameObject newObstacle = GetObstacle(newObstacleType);

        /*
        GameObject newObstacle = GetObstacleFromPool();
        */
        newObstacle.transform.position = transform.position;

        int radialPosition = _lastRadialPosition;
        if (_lastAllowsOffset)
        {
            radialPosition = Random.Range(_lastRadialPosition - m_radialSpread, _lastRadialPosition + m_radialSpread + 1);
        }
        
        rotationVector.z = GetRotation(radialPosition);
        newObstacle.transform.rotation = Quaternion.Euler(rotationVector);
        //newObstacle.name += radialPosition;
        newObstacle.SetActive(true);
        newObstacle.SendMessage("InitObstacle");
        newObstacle.transform.SetParent(this.transform);

        _lastAllowsOffset = newObstacle.GetComponent<ObstacleManager>()._allowNextOffset;
        _lastRadialPosition = radialPosition + newObstacle.GetComponent<ObstacleManager>().m_EndRadialOffset;

        print("Spawned: <color=Green>" + newObstacleType._typeID + "</color> at <color=Blue>" + radialPosition + "</color>. Next radial root at <color=Blue>" + _lastRadialPosition + "</color>");
    }


    private ObstacleType GetRandomObstacleType()
    {
        int randomIndex = Random.Range(0, m_ObstaclePrefabs.Length);
        return m_ObstaclePrefabs[randomIndex];
    }


    /**
    * Gets an obstacle of the given type from the pool.
    * If there is no inactive obstacle of that type in the pool,
    * it creates a new obstacle.
    */
    private GameObject GetObstacle(ObstacleType newObstacleType)
    {
        // Try to get it from the pool
        for (int i = 0; i < ObstaclePool.Count - 1; i++)
        {
            if (!ObstaclePool[i].activeInHierarchy)
            {
                // Make sure it matches the selected type
                if (ObstaclePool[i].GetComponent<ObstacleManager>()._typeID == newObstacleType._typeID)
                {
                    return ObstaclePool[i];
                }
            }
        }

        // If not, create a new one
        GameObject newObstacle = (GameObject)Instantiate(newObstacleType._prefab);
        ObstaclePool.Add(newObstacle);
        return newObstacle;
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
    /*
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
    */
}
