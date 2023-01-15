using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerStatus : StatusModelBase
{
   public IReactiveCollection<CardBaseClass> CardList => _cardList;
   private ReactiveCollection<CardBaseClass> _cardList = new ReactiveCollection<CardBaseClass>();
   
   


}
