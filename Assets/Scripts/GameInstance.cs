using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton to manage scenes and keep a reference to the player.
/// </summary>
public class GameInstance : Singleton<GameInstance>
{
    protected GameInstance () {}

    ///<summary>Reference to the player</summary>
    private static GameObject _player;

    ///<summary>Reference to the current level Game Manager</summary>
    private static GameManager _currentGameManager;

    /// <summary>
    /// Get a player reference from anywhere in the game,
    /// since this is a Singleton class.
    /// </summary>
    /// <returns>the GameObject of the player</returns>
    public static GameObject GetPlayer()
    {
        // If the player has not been found yet, retrieve it from the game manager
        if (_player == null)
        {
            _player = _currentGameManager._playerManager._instance;
        }
        return _player;
    }


    /// <summary>
    /// Returns the instance of the current level game manager.
    /// The game manager holds info of the current game state.
    /// </summary>
    /// <returns>The current level game manager instance</returns>
    public static GameManager GetCurrentGameManager()
    {
        return _currentGameManager;
    }


    /// <summary>
    /// When the Game Manager is loaded, 
    /// it registers itself into the GameInstance with this method.
    /// </summary>
    /// <param name="pGameManager">Current level Game Manager</param>
    public static void SetCurrentGameManager(GameManager pGameManager)
    {
        _currentGameManager = pGameManager;
    }


    /// <summary>
    /// Load the desired level.
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
    /// Show the GameOver screen over the current scene.
    /// </summary>
    public static void GameOver ()
    {
        LoadLevelAdditive("MainMenu_Additive");
    }
}
