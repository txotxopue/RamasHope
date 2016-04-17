using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuWindowManager : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "test";


    public void NewGame()
    {
        SceneManager.LoadScene(sceneName);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
