using UnityEngine;
using System.Collections;

public class GameInstance : Singleton<GameInstance>
{
    protected GameInstance () {}

    public string myGlobalVar = "Whatever";
    public GameObject PlayerToInstance;
}
