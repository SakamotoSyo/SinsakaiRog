using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class CardEvent : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("カーソルを合わせたときのカードの大きさ倍率")]
    [SerializeField] float _cardPickSize;
    [SerializeField] RectTransform _rectpos;
    [SerializeField] CardController _controller;

    bool _moveFenish = false;
    Vector3 _savePos;

    private void Start()
    {
        _savePos = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectpos.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DOTween.To(() => transform.localPosition,
            x => transform.localPosition = x,
            _savePos,0.5f)
            .OnComplete(() => 
            {
                transform.localPosition = _savePos;
                _moveFenish = false;
            });
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _controller.CardAnimation.SelectAnim(true);
        _rectpos.localScale *= _cardPickSize;
        if (!_moveFenish)
        {
            _savePos = this.transform.localPosition;
        }
        _moveFenish = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _controller.CardAnimation.SelectAnim(false);
        _rectpos.localScale /= _cardPickSize;
    }


}
