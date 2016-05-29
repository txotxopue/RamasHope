using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Base class for other windows.
/// Implements the basic methods to Open and Close a Window.
/// </summary>
public class BaseWindow : MonoBehaviour
{
    /// <summary>The WindowManager responsible for this window</summary>
    public static WindowManager _manager;
    /// <summary>The item to be first selected when this window is opened (if any)</summary>
    [SerializeField]
    protected GameObject _firstSelected;

    /// <summary>The next window in the sequence (if any)</summary>
    [SerializeField]
    protected EWindows _nextWindow;
    /// <summary>The previous window in the sequence (if any)</summary>
    [SerializeField]
    protected EWindows _previousWindow;

    /// <summary>EventSystem on this scene</summary>
    protected EventSystem eventSystem
    {
        get { return GameObject.Find("EventSystem").GetComponent<EventSystem>(); }
    }


    /// <summary>
    /// Method to give focus to the default item when this window is opened.
    /// </summary>
    protected virtual void OnFocus()
    {
        if (_firstSelected != null)
        {
            eventSystem.SetSelectedGameObject(_firstSelected);
        }
    }


    /// <summary>
    /// Switch this Window ON/OFF when it is opened/closed from the WindowManager.
    /// </summary>
    /// <param name="value">True to display the window, false to hide it</param>
    protected virtual void Display(bool value)
    {
        gameObject.SetActive(value);
        
    }


    /// <summary>
    /// Method to Open this window.
    /// It displays the window and gives focus to the default item.
    /// </summary>
    public virtual void Open()
    {
        Display(true);
        OnFocus();
    }


    /// <summary>
    /// Method to Close this window, hiding it.
    /// </summary>
    public virtual void Close()
    {
        Display(false);
    }


    // We hide the Window by default in Awake
    protected virtual void Awake()
    {
        Close();
    }


    /// <summary>
    /// If there is a window next in a sequence menu, open it.
    /// </summary>
    public void OnNextWindow()
    {
        _manager.Open((int)_nextWindow - 1);
    }


    /// <summary>
    /// If there is a window previous in a sequence menu, open it.
    /// </summary>
    public void OnPreviousWindow()
    {
        _manager.Open((int)_previousWindow - 1);
    }
}
