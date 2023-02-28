using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusSaveData
{
    public float MaxHp;
    public float Currenthp;
    public float Defence;
    public float Nowcost;
    public float MaxCost;
    public float Gold;
    public List<CardBaseClass> DeckCardList;

    public PlayerStatusSaveData() 
    {
        DeckCardList = new List<CardBaseClass>();
    } 
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
