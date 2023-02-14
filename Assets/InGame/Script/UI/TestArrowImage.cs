using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestArrowImage : MonoBehaviour
{
    private Tween _saveTween;

    private void OnEnable()
    {
       RectTransform _rectPos = GetComponent<RectTransform>();
       _saveTween = _rectPos.DOAnchorPos(new Vector2(_rectPos.localPosition.x, _rectPos.localPosition.y + 4), 0.8f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.Kill(_saveTween);
    }
}
