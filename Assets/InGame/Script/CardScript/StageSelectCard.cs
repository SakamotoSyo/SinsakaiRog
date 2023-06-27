using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class StageSelectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("カーソルを合わせたときのカードの大きさ倍率")]
    [SerializeField] private float _cardPickSize;
    [SerializeField] private RectTransform _rectpos;
    [SerializeField] private CardController _controller;
    [SerializeField] private UIOutline _uiLine;

    private bool _moveFenish = false;
    private Vector3 _localScale;
    private Vector3 _savePos;
    private StageType _stageType;
    private Tween _clickTween;
    private Tween _pointEnterTween;

    private void Awake()
    {
        _moveFenish = true;
        _localScale = _rectpos.localScale;
    }

    private void Start()
    {

    }

    private async void OnEnable()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.Delay(TimeSpan.FromSeconds(0.1), cancellationToken: token);
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
        _clickTween = DOTween.To(() => transform.localPosition,
              x => transform.localPosition = x,
              _savePos, 0.5f)
              .OnStart(() => _moveFenish = true)
              .OnComplete(() =>
              {
                  transform.localPosition = _savePos;
                  _moveFenish = false;
              });
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("来た");
        _pointEnterTween = _uiLine.DOColor(new Color(_uiLine.color.r, _uiLine.color.g, _uiLine.color.b, 1), 1.5f)
               .SetLoops(-1, LoopType.Yoyo);
        _rectpos.localScale *= _cardPickSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointEnterTween.Kill();
        _uiLine.color = new Color(_uiLine.color.r, _uiLine.color.g, _uiLine.color.b, 0.2f);
        _rectpos.localScale = _localScale;
    }

    public void SetStageType(StageType stageType) 
    {
        _stageType = stageType;
    }

    private void OnDestroy()
    {
        _clickTween.Kill();
    }
}
