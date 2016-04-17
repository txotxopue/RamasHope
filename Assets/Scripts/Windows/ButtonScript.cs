using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private Image selectedImage;

    public void OnPointerEnter (PointerEventData eventData)
    {
        selectedImage.enabled = true;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        selectedImage.enabled = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        selectedImage.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selectedImage.enabled = false;
    }
}
