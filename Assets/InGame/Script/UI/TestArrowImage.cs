using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestArrowImage : MonoBehaviour
{
    private void OnEnable()
    {
       RectTransform _rectPos = GetComponent<RectTransform>();
            _rectPos.DOAnchorPos(new Vector2(_rectPos.localPosition.x, _rectPos.localPosition.y + 4), 0.8f)
            .SetLink(gameObject)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
