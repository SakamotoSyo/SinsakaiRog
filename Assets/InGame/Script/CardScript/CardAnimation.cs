using UnityEngine;
using System;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

[Serializable]
public class CardAnimation 
{
    [SerializeField] private Animator _anim;
    [AnimationParameter]
    [SerializeField] private string _selectParm;
    [Tooltip("ドローした際のAnimationのPositionを変えれる")]
    [SerializeField] private Vector3 _drawAnimOffSet;
    [SerializeField] private CardEvent _cardEvent;
    [SerializeField] private Vector3 _ThrowPos;
    [SerializeField] private bool _isDrawAnim;
    private Transform _parentTransform;
    private Tween _tween;

    public void Init(Animator anim) 
    {
        _anim = anim;
    }

    /// <summary>
    /// selectAnimationを再生させるかどうか
    /// </summary>
    /// <param name="play">Animationを再生させるかどうか</param>
    public void SelectAnim(bool play) 
    {
        _anim.SetBool(_selectParm, play);
    }

    /// <summary>
    /// 生成される場所のTransform
    /// </summary>
    /// <param name="transform"></param>
    public void SetParentTransform(Transform transform) 
    {
        _parentTransform = transform;
    }

    public void DrawAnim(Transform transform) 
    {
        if (_isDrawAnim) return;
       _tween = DOTween.To(() => transform.localPosition - _parentTransform.localPosition - _drawAnimOffSet,
            x => transform.localPosition = x,
            transform.localPosition, 0.5f)
            .OnStart(() => _cardEvent.enabled = false)
            .OnComplete(() => _cardEvent.enabled = true);
    }

    public async UniTask ThrowAnim(Transform transform, CancellationToken token) 
    {
       await DOTween.To(() => transform.localPosition,
            x => transform.localPosition = x,
            _ThrowPos, 0.6f).ToUniTask(cancellationToken: token);
    }
}
