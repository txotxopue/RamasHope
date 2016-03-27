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
    ///<summary>Prefab to instantiate player</summary>
    public GameObject _playerPrefab;
    ///<summary>Player manager describing the player and his spawning point</summary>
    public PlayerManager _playerManager;
    ///<summary>Reference to the obstacle spawner. We need it to turn on/off the spawning</summary>
    public ObstacleSpawner _obstacleSpawner;
    ///<summary>Reference to the container of the information text layers</summary>
    public GameObject _textContainer;

    ///<summary>How much time to yield before starting to drain player's energy</summary>
    public float _startDelay = 3f;
    ///<summary>Yield object to drain player's energy. To give the tubes time to travel towards the player.</summary>
    private WaitForSeconds _startWait;


    private void Start()
    {
        _startWait = new WaitForSeconds(_startDelay);
        SpawnPlayer();

        StartCoroutine(GameLoop());
    }


    /// <summary>
    /// Spawns and setups the player
    /// </summary>
    private void SpawnPlayer()
    {
        _playerManager.m_Instance =
            Instantiate(_playerPrefab, _playerManager.m_SpawnPoint.position, _playerManager.m_SpawnPoint.rotation) as GameObject;
        _playerManager.m_Instance.name = "Player";
        _playerManager.Setup();
    }


    /// <summary>
    /// Level loop managing the different states of this round.
    /// </summary>
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(LevelStarting());
        yield return StartCoroutine(LevelPlaying());
        yield return StartCoroutine(LevelEnding());
        
        /*
        if (m_GameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
        */
    }


    /// <summary>
    /// Activates obstacle spawning, disables player's energy draining,
    /// displays a GET READY text, and finally waits a certain amount of time given by _startDelay.
    /// </summary>
    private IEnumerator LevelStarting()
    {
        _obstacleSpawner.SetSpawnActive(true);
        GameInstance.Instance.GetPlayer().GetComponentInChildren<EnergyManager>()._bEnergyDrain = false;
        DisplayText("GET READY!");
        yield return _startWait;
    }


    /// <summary>
    /// Activates energy draining and loops til the player is destroyed.
    /// </summary>
    private IEnumerator LevelPlaying()
    {
        GameInstance.Instance.GetPlayer().GetComponentInChildren<EnergyManager>()._bEnergyDrain = true;
        EmptyText();

        while (_playerManager.m_Instance.activeSelf)
        {
            yield return null;
        }
    }


    /// <summary>
    /// Displays a GAME OVER text and loads the game over scene.
    /// </summary>
    private IEnumerator LevelEnding()
    {
        DisplayText("GAME OVER");
        GameInstance.Instance.GameOver();
        yield return null;
    }


    /// <summary>
    /// Displays the given text on the different layers of the text container.
    /// </summary>
    /// <param name="pText">Text to display</param>
    private void DisplayText(string pText)
    {
        foreach (Text text in _textContainer.transform.GetComponentsInChildren<Text>())
        {
            text.text = pText;
        }
    }


    /// <summary>
    /// Deletes all text from the text container's layers.
    /// </summary>
    private void EmptyText()
    {
        foreach (Text text in _textContainer.transform.GetComponentsInChildren<Text>())
        {
            text.text = String.Empty;
        }
    }
}

/*
private void SetCameraTargets()
{
    Transform[] targets = new Transform[m_Tanks.Length];

    for (int i = 0; i < targets.Length; i++)
    {
        targets[i] = m_Tanks[i].m_Instance.transform;
    }

    m_CameraControl.m_Targets = targets;
}*/

/*
private bool OneTankLeft()
{
    int numTanksLeft = 0;

    for (int i = 0; i < m_Tanks.Length; i++)
    {
        if (m_Tanks[i].m_Instance.activeSelf)
            numTanksLeft++;
    }

    return numTanksLeft <= 1;
}*/

/*
private TankManager GetRoundWinner()
{
for (int i = 0; i < m_Tanks.Length; i++)
{
    if (m_Tanks[i].m_Instance.activeSelf)
        return m_Tanks[i];
}

return null;
}
*/
/*
private TankManager GetGameWinner()
{
    for (int i = 0; i < m_Tanks.Length; i++)
    {
        if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
            return m_Tanks[i];
    }

    return null;
}*/

/*
private string EndMessage()
{
string message = "DRAW!";

if (m_RoundWinner != null)
    message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

message += "\n\n\n\n";

for (int i = 0; i < m_Tanks.Length; i++)
{
    message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
}

if (m_GameWinner != null)
    message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

return message;
}*/

/*
private void ResetAllTanks()
{
for (int i = 0; i < m_Tanks.Length; i++)
{
    m_Tanks[i].Reset();
}
}*/

/*
private void EnableTankControl()
{
for (int i = 0; i < m_Tanks.Length; i++)
{
    m_Tanks[i].EnableControl();
}
}*/

/*
    private void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].DisableControl();
        }
    }*/
