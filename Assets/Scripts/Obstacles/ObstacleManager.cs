﻿using UnityEngine;


/// <summary>
/// Class that manages each obstacle prefab.
/// Initializes the obstacles when they are reused and 
/// set them inactive in the pool when they are no more needed.
/// </summary>
public class ObstacleManager : MonoBehaviour
{

    ///<summary>Type of the obstacle</summary>
    [Tooltip("Type of the obstacle")]
    [SerializeField]
    private EObstacleType _typeID;
    ///<summary>Type of the obstacle</summary>
    public EObstacleType TypeID
    {
        get { return _typeID; }
    }

    ///<summary>Does this obstacle allow the next one to be offset in angle?</summary>
    [Tooltip("Does this obstacle allow the next one to be offset in angle?")]
    [SerializeField]
    private bool _allowNextOffset = false;
    ///<summary>Does this obstacle allow the next one to be offset in angle?</summary>
    public bool AllowNextOffset
    {
        get { return _allowNextOffset; }
    }

    ///<summary>How many positions is the end of this obstacle offset (0-23)</summary>
    [Tooltip("How many positions is the end of this obstacle offset (0-23)")]
    [SerializeField]
    private int _endRadialOffset = 0;
    ///<summary>How many positions is the end of this obstacle offset (0-23)</summary>
    public int EndRadialOffset
    {
        get { return _endRadialOffset; }
    }

    ///<summary>Pickups container under this obstacle prefab</summary>
    [Tooltip("Pickups container under this obstacle prefab")]
    [SerializeField]
    private Transform _pickups;


    // Find the pickups
    void Awake()
    {
        _pickups = transform.FindChild("Pickups");
    }


    /// <summary>
    /// Reactivate the pickups under the container.
    /// Called when we need to reuse one obstacle from the pool.
    /// </summary>
    public void InitObstacle()
    {
        if (_pickups == null)
        {
            print("No pickups to initialize");
        }
        foreach (Transform child in _pickups)
        {
            child.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// When we collide with the killzone,
    /// we prepare the obstacle to return to the pool.
    /// </summary>
    /// <param name="other">Collider which we collide with. Probably we should check if this is the Killzone.</param>
    void OnTriggerEnter(Collider other)
    {
        ReturnToPool();
    }


    /// <summary>
    /// Returning to the pool actually means setting the obstacle inactive,
    /// since the obstacles are always in the pool. Later, when we need one, 
    /// we just look for an inactive one.
    /// </summary>
    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
