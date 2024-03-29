using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class CardEvent : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("カーソルを合わせたときのカードの大きさ倍率")]
    [SerializeField] private float _cardPickSize;
    [SerializeField] private RectTransform _rectpos;
    [SerializeField] private CardController _controller;
    [SerializeField] private UIOutline _uiLine;

    private bool _moveFenish = false;
    private Vector3 _localScale;
    private Vector3 _savePos;

    private void Awake()
    {
        _moveFenish = true;
        _localScale = _rectpos.localScale;
    }

    private void Start()
    {
       
    }

    private void OnEnable()
    {
        _moveFenish = false;
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
            DOTween.To(() => transform.localPosition,
            x => transform.localPosition = x,
            _savePos,0.5f)
            .OnStart(() => _moveFenish = true)
            .SetLink(gameObject)
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
        _rectpos.localScale = _localScale;
    }
}
