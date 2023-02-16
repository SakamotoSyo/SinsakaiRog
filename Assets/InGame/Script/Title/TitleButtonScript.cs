using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleButtonScript : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIOutline _uiOutLine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _uiOutLine.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _uiOutLine.enabled = false;
    }
}
