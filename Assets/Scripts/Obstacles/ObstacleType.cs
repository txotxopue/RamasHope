using System;
using UnityEngine;

/// <summary>
/// Structure associating an obstacle ID with its prefab.
/// </summary>
[Serializable]
public class ObstacleType
{
    public EObstacleType _typeID;
    public GameObject _prefab;
}
