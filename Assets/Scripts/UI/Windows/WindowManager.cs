using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour
{
    /// <summary>List of windows in the scene</summary>
    [HideInInspector]
    public BaseWindow[] _windows;
    /// <summary>Current window ID from the list</summary>
    [SerializeField]
    private EWindows _currentWindowID;
    /// <summary>Default window ID from the list</summary>
    [SerializeField]
    private EWindows _defaultWindowID;


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
        _currentWindowID = ListIDToWindow(value);
        ToggleVisibility(value);

        return GetWindow(value);
    }


    /// <summary>
    /// Opens the given window, closes the rest,
    /// sets the opened window as the current one and returns the window.
    /// </summary>
    /// <param name="pWindow">Window enum value of the window to open</param>
    /// <returns>The window opened</returns>
    public BaseWindow Open(EWindows pWindow)
    {
        return Open(WindowToListID(pWindow));
    }


    /// <summary>
    /// Closes the active window(if any).
    /// </summary>
    public void Close()
    {
        if (_currentWindowID != EWindows.None)
        {
            BaseWindow window = GetWindow(WindowToListID(_currentWindowID));
            if (window != null && window.gameObject.activeSelf)
            {
                window.Close();
                _currentWindowID = EWindows.None;
            }
        }
    }


    /// <summary>
    /// Translates the Windows Enum value 
    /// to the index of the Window List.
    /// </summary>
    /// <param name="pWindow">Windows Enum value of the window</param>
    /// <returns>int index of the window list</returns>
    private int WindowToListID(EWindows pWindow)
    {
        return (int)pWindow - 1;
    }


    /// <summary>
    /// Translates the index in the window list to
    /// the corresponding Windows Enum value.
    /// </summary>
    /// <param name="pWindowListID">int index of the window in the list</param>
    /// <returns>Windows Enum value</returns>
    private EWindows ListIDToWindow(int pWindowListID)
    {
        return (EWindows)(pWindowListID + 1);
    }


    // Registers the manager and opens the default window
    void Start()
    {
        BaseWindow._manager = this;
        Open(WindowToListID(_defaultWindowID));
    }
}
