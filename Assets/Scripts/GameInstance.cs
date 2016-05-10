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

    private static GameManager _currentGameManager;

    /// <summary>
    /// Get a player reference.
    /// </summary>
    /// <returns>the GameObject of the player</returns>
    public static GameObject GetPlayer()
    {
        // If the player has not been found yet, go find him in the hierarchy
        if (m_Player == null)
        {
            m_Player = GameObject.Find("Player");
        }
        return m_Player;
    }


    public static GameManager GetCurrentGameManager()
    {
        return _currentGameManager;
    }


    public static void SetCurrentGameManager(GameManager pGameManager)
    {
        _currentGameManager = pGameManager;
    }


    /// <summary>
    /// Load the desired level.
    /// It also restores timeScale, in case it has been halted.
    /// </summary>
    /// <param name="level">The name of the level to load</param>
    public static void LoadLevel (string level)
    {
        SceneManager.LoadScene(level);
    }


    /// <summary>
    /// Load the desired level in additive mode.
    /// </summary>
    /// <param name="level">The name of the level to load</param>
    public static void LoadLevelAdditive(string level)
    {
        SceneManager.LoadScene(level,LoadSceneMode.Additive);
    }


    /// <summary>
    /// Quit the game.
    /// </summary>
    public static void QuitGame ()
    {
        print("Exiting game...");
        Application.Quit();
    }


    /// <summary>
    /// Show the GameOver screen over the current scene and halt time.
    /// </summary>
    public static void GameOver ()
    {
        LoadLevelAdditive("MainMenu_Additive");
    }
}
