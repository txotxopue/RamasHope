using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Spawns obstacles at intervals of _timeToSpawn.
/// Has a list of ObstacleType and chooses randomly
/// which one to spawn. Also manages a pool of obstacles,
/// so spawning doesn't always mean create a new obstacle.
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [Header("List of obstacle prefabs")]
    ///<summary>List of possible ObstacleType to spawn</summary>
    public ObstacleType[] _obstaclePrefabs;

    [Header("Spawner variables")]
    ///<summary>Interval in seconds between obstacles</summary>
    public float _timeToSpawn = 2f;
    ///<summary>Coroutine invoked to spawn the obstacles at the given _timeToSpawn intervals</summary>
    private IEnumerator _spawningCoroutine;

    ///<summary>Rotation vector for the next obstacle</summary>
    private Vector3 _rotationVector;

    ///<summary>Number of radial positions that an obstacle can offset from the previous one</summary>
    public int _radialSpread = 2;
    ///<summary>Does the last obstacle allow the next one to spawn offset by _radialSpread?</summary>
    private bool _lastAllowsOffset = false;
    ///<summary>Radial position of the last obstacle</summary>
    private int _lastRadialPosition = 0;

    ///<summary>Is this spawner active?</summary>
    private bool _bSpawnIsActive = false;
    [Header ("Obstacle Pool")]
    ///<summary>Obstacle pool containing all the obstacles created. Needed to be able to reuse the ones no longer in player's view</summary>
    public List<GameObject> ObstaclePool;


    void Awake()
    {
        ObstaclePool = new List<GameObject>();
    }


    // Use this for initialization
    void Start ()
    {
        // We set the rotation to the rotation of the spawner
        _rotationVector = transform.rotation.eulerAngles;
        _bSpawnIsActive = false;
	}
	

    /// <summary>
    /// Switch the spawner On/Off.
    /// </summary>
    /// <param name="pIsActive">true to activate the spawner, false to deactivate it.</param>
    public void SetSpawnActive(bool pIsActive)
    {
        _bSpawnIsActive = pIsActive;
        if (_bSpawnIsActive)
        {
            _spawningCoroutine = SpawningCoroutine();
            StartCoroutine(_spawningCoroutine);
        }
        else
        {
            StopCoroutine(_spawningCoroutine);
        }
    }


    public IEnumerator SpawningCoroutine()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(_timeToSpawn);
        }
    }


    // We update the time counter and spawn 
    void Update ()
    {
        /*
        if (_bSpawnIsActive)
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _timeToSpawn)
            {
                //print("Spawned");
                _timeElapsed -= _timeToSpawn;
                SpawnObstacle();
            } 
        }
        */
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
            radialPosition = Random.Range(_lastRadialPosition - _radialSpread, _lastRadialPosition + _radialSpread + 1);
        }
        
        _rotationVector.z = GetRotation(radialPosition);
        newObstacle.transform.rotation = Quaternion.Euler(_rotationVector);
        //newObstacle.name += radialPosition;
        newObstacle.SetActive(true);
        newObstacle.SendMessage("InitObstacle");
        newObstacle.transform.SetParent(this.transform);

        _lastAllowsOffset = newObstacle.GetComponent<ObstacleManager>()._allowNextOffset;
        _lastRadialPosition = radialPosition + newObstacle.GetComponent<ObstacleManager>()._endRadialOffset;

        //print("Spawned: <color=Green>" + newObstacleType._typeID + "</color> at <color=Blue>" + radialPosition + "</color>. Next radial root at <color=Blue>" + _lastRadialPosition + "</color>");
    }


    private ObstacleType GetRandomObstacleType()
    {
        int randomIndex = Random.Range(0, _obstaclePrefabs.Length);
        return _obstaclePrefabs[randomIndex];
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
}
