using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
[CreateAssetMenu(fileName = "CardData", menuName = "SakamotoScriptable/CradData")]
public class CardBaseClass : ScriptableObject
{
    public int ID => _id;
    private int _id;
    public string Name => _name; 
    private string _name;
    private float CardDefence => _cardDefence;
    private float _cardDefence;
    public float CardCost => _cardCost;
    private float _cardCost;
    public TargetType Tartget => _target;
    [SerializeField] private TargetType _target;
    public string CardDescription => _cardDescription;
    [Tooltip("カードに関する説明")]
    private string _cardDescription;
    public Sprite CardSprite => _cardSprite;
    [SerializeField] private Sprite _cardSprite;
    
    [SerializeField] List<EffectData> _effect = new List<EffectData>();


    public CardBaseClass(int id, string name, float defence, string target, float cost, string Description, Sprite sp, List<EffectData> effectBase)
    {
        _id = id;
        _cardDefence = defence;
        _cardCost = cost;
        _target = Enum.Parse<TargetType>(target);
        _cardDescription = Description;
        _name = name;
        _cardSprite = sp;
        _effect = effectBase;
    }   
    
    public void Init(int id, string name, float defence, string target, float cost, string Description, Sprite sp, List<EffectData> effectBase)
    {
        _id = id;
        _cardDefence = defence;
        _cardCost = cost;
        _target = Enum.Parse<TargetType>(target);
        _cardDescription = Description;
        _name = name;
        _cardSprite = sp;
        _effect = effectBase;
    }

    /// <summary>
    /// 設定されたカードの効果を使う
    /// </summary>
    public void UseEffect(PlayerController playCon, EnemyController enemyCon, TargetType Target) 
    {
        //カードに設定した効果を順に発動
        for (int i = 0; i < _effect.Count; i++) 
        {
            _effect[i].CardEffect.UseEffect(playCon, enemyCon, _effect[i].Power, Tartget);
        }
    }
}

[System.Serializable]
public class EffectData
{
    public IEffect CardEffect => _effect;
    [SerializeReference,SubclassSelector]
    private IEffect _effect;
    public float Power => _power;
    [SerializeField] private float _power;

    public EffectData(IEffect effect, float power) 
    {
        _effect = effect;
         _power = power;
    }
}

public enum TargetType 
{
    Player,
    Enemy,
}
