using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using VContainer.Unity;
using VContainer;


[Serializable]
public class PlayerStatus : StatusModelBase, IPlayerStatus
{
    [SerializeField] private float _maxCost;
    [Tooltip("カードに使用の際に必要なコスト")]
    private ReactiveProperty<float> _cost = new();
    private ReactiveProperty<float> _gold = new();
    [Tooltip("手札のカードリスト")]
    private ReactiveCollection<CardBaseClass> _handCardList = new();
    [Tooltip("山札のカードリスト")]
    private ReactiveCollection<CardBaseClass> _deckCardList = new();
    /// <summary>デバック用変数</summary>
    [SerializeField] private List<CardBaseClass> _deckCopyList = new();
    [Tooltip("捨て札を貯めておくList")]
    private ReactiveCollection<CardBaseClass> _discardedCard = new();

    [Inject]
    public PlayerStatus() 
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        _maxCost = 3;
        _deckCardList.Add(DataBaseScript.CardBaseClassList[2]);
        for (int i = 0; i < 4; i++) 
        {
            _deckCardList.Add(DataBaseScript.CardBaseClassList[0]);
            _deckCardList.Add(DataBaseScript.CardBaseClassList[1]);
        }
        //_deckCardList = new ReactiveCollection<CardBaseClass>(DataBaseScript.CardBaseClassList);
        //_deckCardList = new ReactiveCollection<CardBaseClass>(_deckCopyList);
        _cost.Value = _maxCost;
    }

    /// <summary>
    /// カードをドローする
    /// </summary>
    public void DrawCard(float num = 1)
    {
        for (int i = 0; i < num; i++)
        {
            if (_deckCardList.Count == 0 && _discardedCard.Count != 0)
            {
                //Drawするカードがなくなった時捨て札を山札に戻す
                for (int j = 0; j < _discardedCard.Count; j++)
                {
                    _deckCardList.Add(_discardedCard[j]);
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
    }

    /// <summary>
    /// カードを捨て札に加える処理
    /// </summary>
    /// <param name="cardBase"></param>
    public void DiscardedCardAdd(CardBaseClass cardBase) 
    {
        _discardedCard.Add(cardBase);
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


    public void AddGold(float gold)
    {
        _gold.Value += gold;
    }

    public void UseGold(float gold)
    {
        _gold.Value -= gold;
    }

    #region　Get関数

    public IObservable<float> GetCostOb()
    {
        return _cost;
    }

    public IReactiveCollection<CardBaseClass> GetHandCardListOb()
    {
        return _handCardList;
    }

    public IReactiveCollection<CardBaseClass> GetDiscardedCountOb()
    {
        return _discardedCard;
    }

    public IReactiveCollection<CardBaseClass> GetDeckCardListOb()
    {
        return _deckCardList;
    }
    public IStatusBase GetStatusBase()
    {
        return this;
    }
    #endregion
}
