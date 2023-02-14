using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerStatusSaveData
{
    public float MaxHp;
    public float Currenthp;
    public float Defence;
    public float Nowcost;
    public float MaxCost;
    public float Gold;
    public List<CardBaseClass> DeckCardList;
    public List<CardBaseClass> HandCardList;
    public List<CardBaseClass> GraveyardCards;
}

public enum PlayerStatusSaveType 
{
    MaxHp,
    CurrentHp,
    Defence,
    Nowcost,
    MaxCost,
    Gold,
    HandCardList,
    GraveyardCards,
    DeckCardList,
}
