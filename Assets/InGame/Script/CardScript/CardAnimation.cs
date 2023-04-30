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
    [Tooltip("ƒhƒ[‚µ‚½Û‚ÌAnimation‚ÌPosition‚ğ•Ï‚¦‚ê‚é")]
    [SerializeField] private Vector3 _drawAnimOffSet;
    [SerializeField] private CardEvent _cardEvent;
    [SerializeField] private Vector3 _ThrowPos;
    private Transform _parentTransform;
    private Tween _tween;

    public void Init(Animator anim) 
    {
        _anim = anim;
    }

    /// <summary>
    /// selectAnimation‚ğÄ¶‚³‚¹‚é‚©‚Ç‚¤‚©
    /// </summary>
    /// <param name="play">Animation‚ğÄ¶‚³‚¹‚é‚©‚Ç‚¤‚©</param>
    public void SelectAnim(bool play) 
    {
        _anim.SetBool(_selectParm, play);
    }

    /// <summary>
    /// ¶¬‚³‚ê‚éêŠ‚ÌTransform
    /// </summary>
    /// <param name="transform"></param>
    public void SetParentTransform(Transform transform) 
    {
        _parentTransform = transform;
    }

    public void DrawAnim(Transform transform) 
    {
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
