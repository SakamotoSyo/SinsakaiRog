using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


[Serializable]
public class PlayerStatus : StatusModelBase, IPlayerStatus
{
    public float MaxCost => _maxCost;
    public float Cost => _cost.Value;
    public IObservable<float> CostOb => _cost;
    public IReactiveCollection<CardBaseClass> DiscardedCount => _discardedCard;
    public IReactiveCollection<CardBaseClass> HandCardList => _handCardList;
    public IReactiveCollection<CardBaseClass> DeckCardList => _deckCardList;


    [SerializeField] private float _maxCost;
    [Tooltip("カードに使用の際に必要なコスト")]
    private ReactiveProperty<float> _cost = new();
    [Tooltip("手札のカードリスト")]
    private ReactiveCollection<CardBaseClass> _handCardList = new();
    [Tooltip("山札のカードリスト")]
    private ReactiveCollection<CardBaseClass> _deckCardList = new();
    /// <summary>デバック用変数</summary>
    [SerializeField] private List<CardBaseClass> _deckCopyList = new();
    [Tooltip("捨て札を貯めておくList")]
    private ReactiveCollection<CardBaseClass> _discardedCard = new();

    public override void Init()
    {
        base.Init();
        _deckCardList = new ReactiveCollection<CardBaseClass>(_deckCopyList);
        _cost.Value = _maxCost;
    }

    /// <summary>
    /// コストを使用する
    /// </summary>
    /// <param name="useCost"></param>
    public bool UseCost(float useCost)
    {
        if (useCost <= _cost.Value)
        {
            _cost.Value -= useCost;
            return true;
        }
        return false;
    }

    public void ResetCost()
    {
        _cost.Value = _maxCost;
    }

    /// <summary>
    /// DataBaseからランダムにカードを追加する
    /// </summary>
    /// <param name="value"></param>
    public void TestAddCardList()
    {
        _deckCardList.Add(DataBaseScript.CardBaseClassList[0]);
    }

    /// <summary>
    /// カードをドローする
    /// </summary>
    public void DrawCard()
    {
        if (_deckCardList.Count == 0 && _discardedCard.Count != 0)
        {
            //Drawするカードがなくなった時捨て札を山札に戻す
            for (int i = 0; i < _discardedCard.Count; i++) 
            {
                _deckCardList.Add(_discardedCard[i]);
            }
            _discardedCard.Clear();
            _handCardList.Add(_deckCardList[0]);
            _deckCardList.RemoveAt(0);
            Debug.Log(_deckCardList.Count);
        }
        else if (_deckCardList.Count == 0 && _discardedCard.Count == 0)
        {
            Debug.LogWarning("山札に戻すカードはありません");
        }
        else 
        {
            _handCardList.Add(_deckCardList[0]);
            _deckCardList.RemoveAt(0);
        }
    }

    /// <summary>
    /// カードを捨て札に加える処理
    /// </summary>
    /// <param name="cardBase"></param>
    public void DiscardedCardAdd(CardBaseClass cardBase) 
    {
        _discardedCard.Add(cardBase);
    }
}
