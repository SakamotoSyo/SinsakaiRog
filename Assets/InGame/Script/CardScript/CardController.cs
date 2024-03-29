using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using System;
using System.Threading;


public class CardController : MonoBehaviour
{
    public CardAnimation CardAnimation => _cardAnimation;
    [SerializeField] private CardAnimation _cardAnimation;

    public CardBaseClass CardBaseClass => _cardBaseClass.Value;
    public IObservable<CardBaseClass> CardBaseClassOb => _cardBaseClass;
    private ReactiveProperty<CardBaseClass> _cardBaseClass = new ReactiveProperty<CardBaseClass>();
    private CancellationToken _cancellationToken;

    private void Awake()
    {
        _cancellationToken = this.GetCancellationTokenOnDestroy();
        _cardAnimation.DrawAnim(transform);
    }

    /// <summary>
    /// カードを捨てる処理の流れ
    /// </summary>
    public async UniTask ThrowCard()
    {
        await _cardAnimation.ThrowAnim(transform, _cancellationToken);
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void SetCardBaseClass(CardBaseClass card)
    {
        _cardBaseClass.Value = card;
    }
}
