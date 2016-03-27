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
        Time.timeScale = 1f;
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
        Time.timeScale = 0f;
        LoadLevelAdditive("MainMenu_Additive");
    }
}
