using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour
{
    /// <summary>List of windows in the scene</summary>
    [HideInInspector]
    public BaseWindow[] _windows;
    /// <summary>Current window ID from the list</summary>
    [SerializeField]
    private int _currentWindowID;
    /// <summary>Default window ID from the list</summary>
    [SerializeField]
    private int _defaultWindowID;


    /// <summary>
    /// Gets the window from the list in the given position.
    /// </summary>
    /// <param name="value">Position of the window to get</param>
    /// <returns>The Window in that position</returns>
    public BaseWindow GetWindow(int value)
    {
        return _windows[value];
    }


    /// <summary>
    /// Opens the window in the given position and closes the rest.
    /// </summary>
    /// <param name="value">Position of the window to open</param>
    private void ToggleVisibility(int value)
    {
        var total = _windows.Length;
        for (int i = 0; i < total; i++)
        {
            var window = _windows[i];
            if (i == value)
            {
                // We need to set the gameObject active from here before we call the Open
                window.gameObject.SetActive(true);
                window.Open();
            }
            else if (window.gameObject.activeSelf)
            {
                window.Close();
            }
        }
    }


    /// <summary>
    /// Opens the given window, closes the rest,
    /// sets the opened window as the current one and returns the window.
    /// </summary>
    /// <param name="value">Position of the window to open in the array</param>
    /// <returns>The opened window</returns>
    public BaseWindow Open(int value)
    {
        if (value < 0 || value >= _windows.Length)
        {
            return null;
        }
        _currentWindowID = value;
        ToggleVisibility(_currentWindowID);

        return GetWindow(_currentWindowID);
    }


    // Registers the manager and opens the default window
    void Start()
    {
        BaseWindow._manager = this;
        Open(_defaultWindowID);
    }
}
