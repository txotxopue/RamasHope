﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameInstance : Singleton<GameInstance>
{
    protected GameInstance () {}

    public string myGlobalVar = "Whatever";
    public static GameObject PlayerToInstance;

    private static GameObject m_Player;

    public GameObject GetPlayer()
    {
        if (m_Player == null)
        {
            //print("couldnt get the player");
            m_Player = GameObject.Find("Player");
        }
        return m_Player;
    }

    public void LoadLevel (string level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1f;
    }

    public void LoadLevelAdditive(string level)
    {
        SceneManager.LoadScene(level,LoadSceneMode.Additive);
    }

    public void QuitGame ()
    {
        print("Exiting game...");
        Application.Quit();
    }

    public void GameOver ()
    {
        Time.timeScale = 0f;
        LoadLevelAdditive("MainMenu_Additive");
    }
}
