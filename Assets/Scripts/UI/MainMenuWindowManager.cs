using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// Class that handles the Main Menu.
/// It implements the methods to bind each to each of the buttons.
/// </summary>
public class MainMenuWindowManager : MonoBehaviour
{
    /// <summary>Name of the scene to load when the New Game button is pressed</summary>
    [SerializeField]
    private string _sceneName = "test";


    public void NewGame()
    {
        SceneManager.LoadScene(_sceneName);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
