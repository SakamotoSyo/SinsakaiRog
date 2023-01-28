using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerStatus : StatusModelBase, IPlayerStatus
{
   [Tooltip("手札のカードリスト")]
   public IReactiveCollection<CardBaseClass> HandCardList => _handCardList;
   private ReactiveCollection<CardBaseClass> _handCardList = new ReactiveCollection<CardBaseClass>();

   [Tooltip("山札のカードリスト")]
   public List<CardBaseClass> DeckCardList => _deckCardList;
   [SerializeField]private List<CardBaseClass> _deckCardList = new List<CardBaseClass>();

    /// <summary>
    /// DataBaseからランダムにカードを追加する
    /// </summary>
    /// <param name="value"></param>
    public void TestAddCardList(int value) 
    {
        for (int i = 0; i < value; i++) 
        {
            //_deckCardList.Add();
        }
    }

    /// <summary>
    /// カードをドローする
    /// </summary>
    public void DrowCard() 
    {
        _handCardList.Add(_deckCardList[0]);
        _deckCardList.RemoveAt(0);
    }


}
