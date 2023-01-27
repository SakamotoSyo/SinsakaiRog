using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
[CreateAssetMenu(fileName = "CardData", menuName = "SakamotoScriptable/CradData")]
public class CardBaseClass : ScriptableObject
{
    private int _id;
    private string _name;
    private float _mainEffectPower;
    private float _subEffectPower;
    private float _cardDefence;
    private float _cardCost;
    public string Tartget => _target;
    private string _target;
    [Tooltip("カードに関する説明")]
    private string _cardDescription;

    private PlayerStatus _playerStatus;
    private EnemyStatus _enemyStatus;

    
    [SerializeField] List<EffectData> _effect = new List<EffectData>();


    public CardBaseClass(int id, string name, float defence, float cost, string Description, string target, List<EffectData> effectBase)
    {
        _id = id;
        _cardDefence = defence;
        _cardCost = cost;
        _target = target;
        _cardDescription = Description;
        _name = name;
        _effect = effectBase;
    }

    /// <summary>
    /// 設定されたカードの効果を使う
    /// </summary>
    public void UseEffect(PlayerStatus player, EnemyStatus enemy) 
    {
        _playerStatus = player;
        _enemyStatus = enemy;
        //カードに設定した効果を順に発動
        for (int i = 0; i < _effect.Count; i++) 
        {
            _effect[i].CardEffect.UseEffect(player, enemy, _effect[i].Power);
        }
    }
}

[System.Serializable]
public struct EffectData
{
    public IEffect CardEffect => _effect;
    private IEffect _effect;
    public float Power => _power;
    private float _power;

    public EffectData(IEffect effect, float power) 
    {
        _effect = effect;
         _power = power;
    }
}
