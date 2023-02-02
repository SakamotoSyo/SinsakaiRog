using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


[Serializable]
public class PlayerStatus : StatusModelBase, IPlayerStatus
{
    public float Cost => _myCost.Value;
    public IObservable<float> CostOb => _myCost; 
    public IReactiveCollection<CardBaseClass> HandCardList => _handCardList;
    public List<CardBaseClass> DeckCardList => _deckCardList;

    [Header("カードに使用の際に必要なコスト")]
    [SerializeField] private ReactiveProperty<float> _myCost;
    [Tooltip("手札のカードリスト")]
    private ReactiveCollection<CardBaseClass> _handCardList = new ReactiveCollection<CardBaseClass>();
    [Tooltip("山札のカードリスト")]
    [SerializeField] private List<CardBaseClass> _deckCardList = new List<CardBaseClass>();

    /// <summary>
    /// コストを使用する
    /// </summary>
    /// <param name="useCost"></param>
    public void UseCost(float useCost) 
    {
        _myCost.Value -= useCost;
        Debug.Log(_myCost);
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
