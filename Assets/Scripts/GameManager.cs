using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class to manage the level flow.
/// Setups the texts, the player and the level.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Player")]
    ///<summary>Prefab to instantiate player</summary>
    [Tooltip("Prefab to instantiate player")]
    [SerializeField]
    private GameObject _playerPrefab;
    ///<summary>Player manager describing the player and his spawning point</summary>
    [Tooltip("Player manager describing the player and his spawning point")]
    public PlayerManager _playerManager;

    [Header("Obstacles")]
    ///<summary>Reference to the obstacle spawner. We need it to turn on/off the spawning</summary>
    [Tooltip("Reference to the obstacle spawner. We need it to turn on/off the spawning")]
    [SerializeField]
    private ObstacleSpawner _obstacleSpawner;
    ///<summary>Maximum speed of the obstacles traveling through the tube</summary>
    [Tooltip("Maximum speed of the obstacles traveling through the tube")]
    [SerializeField]
    private float _maxTubeSpeed = 100f;
    ///<summary>Current speed of the obstacles traveling through the tube</summary>
    private float _currentTubeSpeed = 0f;

    [Header("UI")]
    ///<summary>Reference to the Window Manager</summary>
    [Tooltip("Reference to the Window Manager")]
    [SerializeField]
    private WindowManager _windowManager;

    [Header("Audio")]
    ///<summary>This level's music theme</summary>
    [Tooltip("This level's music theme")]
    [SerializeField]
    private AudioClip _levelMusicTheme;


    [Header("Game Flow")]
    ///<summary>How much time to yield before starting to drain player's energy</summary>
    [Tooltip("How much time to yield before starting to drain player's energy")]
    [SerializeField]
    private float _startDelay = 3f;
    ///<summary>Yield object to drain player's energy. To give the tubes time to travel towards the player.</summary>
    private WaitForSeconds _startWait;


    private void Awake()
    {
        GameInstance.SetCurrentGameManager(this); //Register this GameManager into the GameInstance
        SpawnPlayer();
    }


    private void Start()
    {
        _startWait = new WaitForSeconds(_startDelay);
        StartCoroutine(GameLoop());
    }


    /// <summary>
    /// Spawns and setups the player.
    /// </summary>
    private void SpawnPlayer()
    {
        _playerManager._instance =
            Instantiate(_playerPrefab, _playerManager._spawnPoint.position, _playerManager._spawnPoint.rotation) as GameObject;
        _playerManager._instance.name = "Player";
        _playerManager.Setup();
    }


    /// <summary>
    /// Level loop managing the different stages of this round.
    /// </summary>
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(LevelReady());
        yield return StartCoroutine(LevelGo());
        yield return StartCoroutine(LevelPlaying());
        yield return StartCoroutine(LevelOver());
    }


    /// <summary>
    /// Activates obstacle spawning, disables player's energy draining,
    /// displays a GET READY text, and finally waits a certain amount of time given by _startDelay.
    /// </summary>
    private IEnumerator LevelReady()
    {
        _obstacleSpawner.SetSpawnActive(true);
        ServiceLocator.GetMusicService().Play(_levelMusicTheme);
        GameInstance.GetPlayer().GetComponentInChildren<EnergyManager>()._bEnergyDrain = false;
        SetFullSpeed(); // Start moving the tube
        _windowManager.Open(EWindows.GetReady);
        yield return _startWait;
    }


    /// <summary>
    /// Activates energy draining and displays the Go! text.
    /// </summary>
    private IEnumerator LevelGo()
    {
        GameInstance.GetPlayer().GetComponentInChildren<EnergyManager>()._bEnergyDrain = true;
        _windowManager.Open(EWindows.Go);
        yield return _startWait;
    }


    /// <summary>
    /// Clears text and loops until the player is destroyed.
    /// </summary>
    private IEnumerator LevelPlaying()
    {
        _windowManager.Close();
        while (_playerManager._instance.activeSelf)
        {
            yield return null;
        }
    }


    /// <summary>
    /// Displays the GAME OVER screen with the menu.
    /// </summary>
    private IEnumerator LevelOver()
    {
        _windowManager.Open(EWindows.GameOverMenu);
        SetTubeSpeed(0f);
        yield return null;
    }


    /// <summary>
    /// Returns the speed in the tube.
    /// </summary>
    /// <returns></returns>
    public float GetTubeSpeed()
    {
        return _currentTubeSpeed;
    }


    /// <summary>
    /// Sets the speed in the tube.
    /// </summary>
    /// <param name="pTubeSpeed">Speed to set</param>
    public void SetTubeSpeed(float pTubeSpeed)
    {
        if (pTubeSpeed <= _maxTubeSpeed && pTubeSpeed >= 0f)
        {
            _currentTubeSpeed = pTubeSpeed;
        }
        else if (pTubeSpeed > _maxTubeSpeed)
        {
            SetFullSpeed();
        }
        else
        {
            _currentTubeSpeed = 0f;
        }


    }


    /// <summary>
    /// Sets the tube speed to the maximum tube speed.
    /// </summary>
    public void SetFullSpeed()
    {
        _currentTubeSpeed = _maxTubeSpeed;
    }


    /// <summary>
    /// Displays the given text on the different layers of the text container.
    /// </summary>
    /// <param name="pText">Text to display</param>
    private void DisplayText(string pText)
    {
        foreach (Text text in _windowManager.transform.GetComponentsInChildren<Text>())
        {
            text.text = pText;
        }
    }


    /// <summary>
    /// Deletes all text from the text container's layers.
    /// </summary>
    private void EmptyText()
    {
        foreach (Text text in _windowManager.transform.GetComponentsInChildren<Text>())
        {
            text.text = String.Empty;
        }
    }
}
