using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class CardBaseClass
{
    public int ID => _id;
    private int _id;
    public string Name => _name; 
    private string _name;
    private float CardDefence => _cardDefence;
    private float _cardDefence;
    public float CardCost => _cardCost;
    private float _cardCost;
    public int Gold => _gold;
    private int _gold;
    public EnhancementData EnhancementData => _cardEnhancement;
    private EnhancementData _cardEnhancement;
    public int NumberReinforcement => _numberReinforcement;
    private int _numberReinforcement;
    public TargetType Tartget => _target;
    [SerializeField] private TargetType _target;
    public string CardDescription => _cardDescription;
    [Tooltip("カードに関する説明")]
    private string _cardDescription;
    public Sprite CardSprite => _cardSprite;
    [SerializeField] private Sprite _cardSprite;

    public List<EffectData> Effect => _effect;
    [SerializeField] List<EffectData> _effect = new List<EffectData>();


    public CardBaseClass(int id = 0, string name = "", float defence = 0, string target = " Player", float cost = 0, 
        string Description = "", Sprite sp = null, int gold = 0, EnhancementData cardEnhancement = default, List<EffectData> effectBase = null)
    {
        _id = id;
        _cardDefence = defence;
        _cardCost = cost;
        _target = Enum.Parse<TargetType>(target);
        _cardDescription = Description;
        _name = name;
        _cardSprite = sp;
        _gold = gold;
        _cardEnhancement = cardEnhancement;
        _effect = effectBase;
    }   
    
    public void Init(CardBaseClass card)
    {
        _id = card.ID;
        _cardDefence = card.CardDefence;
        _cardCost = card.CardCost;
        _target = card.Tartget;
        _cardDescription = card.CardDescription;
        _name = card.Name;
        _cardSprite = card.CardSprite;
        _gold = card.Gold;
        _cardEnhancement = card.EnhancementData;
        _effect = new List<EffectData>(card.Effect);
    }

    /// <summary>
    /// コストを減らす
    /// </summary>
    public void DecreasedCost() 
    {
        _cardCost -= int.Parse(_cardEnhancement.CardEnhancementNum);
    }

    /// <summary>
    /// 強化した回数を一回増やす
    /// </summary>
    public void SetEnhancementNum() 
    {
        _numberReinforcement++;
        _name += "<color=blue>＋</color>";
    }

    /// <summary>
    /// 一番最初の効果の強さを上げる
    /// </summary>
    public void IncreaseEffectPower()
    {
        _effect[0] = new EffectData(_effect[0].CardEffect, _effect[0].Power + int.Parse(_cardEnhancement.CardEnhancementNum));
        var num = _cardDescription.IndexOf(">");
        StringBuilder sb = new StringBuilder();
        sb.Append(_cardDescription);
        sb.Remove(num + 1, (_effect[0].Power - 1).ToString().Length);
        sb.Insert(num + 1, _effect[0].Power.ToString());
        _cardDescription = sb.ToString();
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

public struct EffectData
{
    public IEffect CardEffect => _effect;
    [SerializeReference,SubclassSelector]
    private IEffect _effect;
    public float Power => _power;
    private float _power;

    public EffectData(IEffect effect, float power) 
    {
        _effect = effect;
         _power = power;
    }
}

public struct EnhancementData 
{
    public string CardEnhancement => _cardEnhancement;
    private string _cardEnhancement;
    public string CardEnhancementNum => _cardEnhancementNum;
    private string _cardEnhancementNum;

    public EnhancementData(string st, string st2) 
    {
        _cardEnhancement = st;
        _cardEnhancementNum = st2;
    }
}

public enum TargetType 
{
    Player,
    Enemy,
}
