using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton to manage scenes and keep a reference to the player.
/// </summary>
public class GameInstance : Singleton<GameInstance>
{
    protected GameInstance () {}

    ///<summary>Reference to the player</summary>
    private static GameObject m_Player;

    ///<summary>Maximum speed of the obstacles traveling through the tube</summary>
    public static float _maxTubeSpeed = 100f;
    ///<summary>Current speed of the obstacles traveling through the tube</summary>
    public static float _currentTubeSpeed = 100f;


    /// <summary>
    /// Get a player reference.
    /// </summary>
    /// <returns>the GameObject of the player</returns>
    public GameObject GetPlayer()
    {
        // If the player has not been found yet, go find him in the hierarchy
        if (m_Player == null)
        {
            m_Player = GameObject.Find("Player");
        }
        return m_Player;
    }


    /// <summary>
    /// Load the desired level.
    /// It also restores timeScale, in case it has been halted.
    /// </summary>
    /// <param name="level">The name of the level to load</param>
    public void LoadLevel (string level)
    {
        SceneManager.LoadScene(level);
        _currentTubeSpeed = _maxTubeSpeed;
    }


    /// <summary>
    /// Load the desired level in additive mode.
    /// </summary>
    /// <param name="level">The name of the level to load</param>
    public void LoadLevelAdditive(string level)
    {
        SceneManager.LoadScene(level,LoadSceneMode.Additive);
    }


    /// <summary>
    /// Quit the game.
    /// </summary>
    public void QuitGame ()
    {
        print("Exiting game...");
        Application.Quit();
    }


    /// <summary>
    /// Show the GameOver screen over the current scene and halt time.
    /// </summary>
    public void GameOver ()
    {
        _currentTubeSpeed = 0f;
        LoadLevelAdditive("MainMenu_Additive");
    }
}
