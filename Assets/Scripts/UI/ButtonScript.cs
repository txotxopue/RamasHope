using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// Script to handle the button via mouse, keyboard or gamepad.
/// Enables or diasbles the "Selected" arrow image.
/// </summary>
public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    /// <summary>Selected arrow Image</summary>
    [SerializeField]
    private Image _selectedImage;

    // Mouse events

    public void OnPointerEnter (PointerEventData eventData)
    {
        _selectedImage.enabled = true;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        _selectedImage.enabled = false;
    }

    // Keyboard/Gamepad events

    public void OnSelect(BaseEventData eventData)
    {
        _selectedImage.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _selectedImage.enabled = false;
    }
}
