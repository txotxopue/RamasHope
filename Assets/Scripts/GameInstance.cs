using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameInstance : Singleton<GameInstance>
{
    protected GameInstance () {}

    public string myGlobalVar = "Whatever";
    public GameObject PlayerToInstance;

    public void LoadLevel (string level)
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame ()
    {
        print("Exiting game...");
        Application.Quit();
    }
}
