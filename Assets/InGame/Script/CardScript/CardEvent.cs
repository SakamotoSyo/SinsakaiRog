using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class CardEvent : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("カーソルを合わせたときのカードの大きさ倍率")]
    [SerializeField] private float _cardPickSize;
    [SerializeField] private RectTransform _rectpos;
    [SerializeField] private CardController _controller;

    private bool _moveFenish = false;
    private Vector3 _savePos;
    private Tween _tween;


    private async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        _savePos = this.transform.localPosition;
    }

    

    public void OnDrag(PointerEventData eventData)
    {
        if (!_moveFenish) 
        {
            _rectpos.position = eventData.position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_moveFenish) 
        {
            _savePos = this.transform.localPosition;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //クリックの終了時クリックし始めたpositionまで戻る
      _tween = DOTween.To(() => transform.localPosition,
            x => transform.localPosition = x,
            _savePos,0.5f)
            .OnStart(() => _moveFenish = true)
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _controller.CardAnimation.SelectAnim(false);
        _rectpos.localScale /= _cardPickSize;
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
