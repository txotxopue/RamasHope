using System;
using UnityEngine;

[Serializable]
public class PlayerManager
{
    /// <summary>Color to tint the ship material</summary>
    [SerializeField]
    private Color _playerColor;
    /// <summary>Point in the tube where the ship will spawn</summary>
    public Transform _spawnPoint;
    /// <summary>Reference to the player prefab instance</summary>
    [HideInInspector]
    public GameObject _instance;


    /// <summary>
    /// Setup the player ship color.
    /// </summary>
    public void Setup()
    {
        MeshRenderer[] renderers = _instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = _playerColor;
        }
    }
}
