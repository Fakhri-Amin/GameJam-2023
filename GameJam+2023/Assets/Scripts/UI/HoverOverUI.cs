using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject hoverOverUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOverUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOverUI.SetActive(false);
    }
}