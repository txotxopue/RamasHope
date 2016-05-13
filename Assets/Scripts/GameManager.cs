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

    [Header("Game State")]
    ///<summary>Time surviving in the tube</summary>
    private float _timer = 0f;
    ///<summary>HUD label containing the time surviving in the tube</summary>
    [Tooltip("HUD label containing the time surviving in the tube")]
    [SerializeField]
    private Text _timerLabel;
    ///<summary>HUD containing the timer</summary>
    [Tooltip("HUD containing the timer")]
    [SerializeField]
    private GameObject _timerHUD;
    ///<summary>True if we are counting time, false if not</summary>
    private bool _bTimerIsOn = false;


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
        StartTimer();
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
        StopTimer();
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
    /// Resets and starts the timer
    /// </summary>
    private void StartTimer()
    {
        _timerHUD.gameObject.SetActive(true);
        _timer = 0f;
        _bTimerIsOn = true;
    }


    /// <summary>
    /// Stops the timer
    /// </summary>
    private void StopTimer()
    {
        _timerHUD.gameObject.SetActive(false);
        _bTimerIsOn = false;
    }


    /// <summary>
    /// Returns the current time in the timer in seconds.
    /// </summary>
    /// <returns>Current time in seconds</returns>
    public float GetTimer()
    {
        return _timer;
    }


    /// <summary>
    /// Returns the current time formatted in two flavors:
    /// with or without the deciseconds.
    /// </summary>
    /// <param name="pWithMillis"></param>
    /// <returns>The formatted time</returns>
    public string GetTimerFormatted(bool pWithDecis)
    {
        int hours = (int)(_timer / 60 / 60);
        int minutes = (int)(_timer / 60);
        int seconds = (int)(_timer % 60);
        if (pWithDecis)
        {
            int decis = (int)(_timer * 10 % 10);
            return string.Format("{0:00}:{1:00}:{2:00}.{3:0}", hours, minutes, seconds, decis);
        }
        else
        {
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        
    }


    private void Update ()
    {
        if (_bTimerIsOn)
        {
            // We update the timer
            _timer += Time.deltaTime;
            _timerLabel.text = GetTimerFormatted(true);
        }
    }
}
