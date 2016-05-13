using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverWindow : MainMenuWindow
{
    /// <summary>Reference to the window element that will contain the timer result</summary>
    [Tooltip("Reference to the window element that will contain the timer result")]
    [SerializeField]
    private Text _timerLabel;


	// Use this for initialization
	void Start ()
    {
        _timerLabel.text = GameInstance.GetCurrentGameManager().GetTimerFormatted(false);
	}
}
