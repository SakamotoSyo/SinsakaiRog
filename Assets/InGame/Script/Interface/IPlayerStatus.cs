using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public interface IPlayerStatus
{
    public IReactiveCollection<CardBaseClass> GetHandCardListOb();
    public IReactiveCollection<CardBaseClass> GetGraveyardCardsCountOb();
    public IReactiveCollection<CardBaseClass> GetDeckCardListOb();
    public ReactiveCollection<CardBaseClass> GetDeckCardList();
    public ReactiveCollection<CardBaseClass> GetGraveyardCardList();
    public IObservable<float> GetGoldOb();
    public IObservable<float> GetCostOb();
    public IStatusBase GetStatusBase();
    public bool UseCost(float useCost);
    public void ResetCost();
    public void AddGold(float gold);
    public bool UseGold(float gold);
    public void DeckInit();
    public void DrawCard(float num = 1);
    public void AddDeckCard(CardBaseClass cardBase);
    public void GraveyardCardsAdd(CardBaseClass cardBase);
    public PlayerStatusSaveData GetPlayerSaveData();
    public void SetPlayerSaveData(PlayerStatusSaveData playerData);

}

