using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// Class that handles the Main Menu.
/// It implements the methods to bind each to each of the buttons.
/// </summary>
public class MainMenuWindow : BaseWindow
{
    /// <summary>Name of the scene to load when the New Game button is pressed</summary>
    [SerializeField]
    private string _sceneName = "test";


    /// <summary>
    /// When the New Game button is pressed, we load the first level.
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(_sceneName);
    }


    /// <summary>
    /// Exits the game when the Exit Game button is pressed.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
