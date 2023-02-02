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
    public IReactiveCollection<CardBaseClass> HandCardList => _handCardList;
    public List<CardBaseClass> DeckCardList => _deckCardList;


    [SerializeField] private float _maxCost;
    [Tooltip("カードに使用の際に必要なコスト")]
    private ReactiveProperty<float> _cost = new();
    [Tooltip("手札のカードリスト")]
    private ReactiveCollection<CardBaseClass> _handCardList = new();
    [Tooltip("山札のカードリスト")]
    [SerializeField] private List<CardBaseClass> _deckCardList = new();

    public override void Init()
    {
        base.Init();
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

    //TODO:ここのRemoveコメントを外す
    /// <summary>
    /// カードをドローする
    /// </summary>
    public void DrowCard()
    {
        _handCardList.Add(_deckCardList[0]);
        //_deckCardList.RemoveAt(0);
    }
}
