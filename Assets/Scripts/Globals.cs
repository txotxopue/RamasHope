using UnityEngine;
using System.Collections;

/// <summary>
/// Enumeration of the different obstacle types.
/// </summary>
public enum EObstacleType
{
    Left,
    Right,
    Forward,
    Scattered,
    Empty
}


/// <summary>
/// Enumeration of the different service types.
/// e.g. in the sound service, we can choose to play with 
/// the default audio, logged audio, or with no sounds.
/// </summary>
public enum EServiceType
{
    Default,
    Logged,
    Null
}
