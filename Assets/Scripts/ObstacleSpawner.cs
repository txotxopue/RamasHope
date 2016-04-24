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
    [SerializeField]
    private ObstacleType[] _obstaclePrefabs;

    [Header("Spawner variables")]
    ///<summary>Interval in seconds between obstacles</summary>
    [SerializeField]
    private float _timeToSpawn = 2f;
    ///<summary>Coroutine invoked to spawn the obstacles at the given _timeToSpawn intervals</summary>
    private IEnumerator _spawningCoroutine;

    ///<summary>Rotation vector for the next obstacle</summary>
    private Vector3 _rotationVector;

    ///<summary>Number of radial positions that an obstacle can offset from the previous one</summary>
    [SerializeField]
    private int _radialSpread = 2;
    ///<summary>Does the last obstacle allow the next one to spawn offset by _radialSpread?</summary>
    private bool _lastAllowsOffset = false;
    ///<summary>Radial position of the last obstacle</summary>
    private int _lastRadialPosition = 0;

    ///<summary>Is this spawner active?</summary>
    private bool _bSpawnIsActive = false;
    [Header ("Obstacle Pool")]
    ///<summary>Obstacle pool containing all the obstacles created. Needed to be able to reuse the ones no longer in player's view</summary>
    [SerializeField]
    private List<GameObject> ObstaclePool;


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


    /// <summary>
    /// Corroutine to spawn obstacles by _timeToSpawn
    /// </summary>
    public IEnumerator SpawningCoroutine()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(_timeToSpawn);
        }
    }


    /* Deprecated in favor of Spawning coroutine
    // We update the time counter and spawn 
    void Update ()
    {
        
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
        
	}
    */


    /// <summary>
    /// Spawns a new obstacle ramdomly from the avalaible types.
    /// It tries to get the obstacle from the pool if possible.
    /// </summary>
    private void SpawnObstacle ()
    {
        ObstacleType newObstacleType = GetRandomObstacleType();
        // Gets the obstacle from the pool or creates a new one
        GameObject newObstacle = GetObstacle(newObstacleType);
        newObstacle.transform.position = transform.position;

        // Get new radial position for the new obstacle if rotation is allowed 
        int radialPosition = _lastRadialPosition;
        if (_lastAllowsOffset)
        {
            radialPosition = Random.Range(_lastRadialPosition - _radialSpread, _lastRadialPosition + _radialSpread + 1);
        }
        // Set the new rotation
        _rotationVector.z = GetRotation(radialPosition);
        newObstacle.transform.rotation = Quaternion.Euler(_rotationVector);
        
        // Activate the obstacle
        newObstacle.SetActive(true);
        newObstacle.SendMessage("InitObstacle");
        newObstacle.transform.SetParent(this.transform);

        // Save the radial offset for the next obstacle
        _lastAllowsOffset = newObstacle.GetComponent<ObstacleManager>()._allowNextOffset;
        _lastRadialPosition = radialPosition + newObstacle.GetComponent<ObstacleManager>()._endRadialOffset;

        //print("Spawned: <color=Green>" + newObstacleType._typeID + "</color> at <color=Blue>" + radialPosition + "</color>. Next radial root at <color=Blue>" + _lastRadialPosition + "</color>");
    }


    /// <summary>
    /// Chooses an obstacle type from the list of prefabs
    /// </summary>
    /// <returns></returns>
    private ObstacleType GetRandomObstacleType()
    {
        int randomIndex = Random.Range(0, _obstaclePrefabs.Length);
        return _obstaclePrefabs[randomIndex];
    }


    /// <summary>
    /// Gets an obstacle of the given type from the pool.
    /// If there is no inactive obstacle of that type in the pool,
    /// it creates a new obstacle.
    /// </summary>
    /// <param name="newObstacleType">Type of the obstacle that we want to retrieve from the pool or create.</param>
    /// <returns>The new obstacle</returns>
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


    /// <summary>
    /// Blocks can spawn in one of 24 circular position.
    /// Given a number from 0 to 23, this function returns the equivalent euler angle
    /// needed to spawn the prefab
    /// </summary>
    /// <param name="pPosition">The radial position of the obstacle</param>
    /// <returns>The angle in Euler float format</returns>
    private float GetRotation(int pPosition)
    {
        return (360f / 24f * pPosition);
    }
}
