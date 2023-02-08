using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using VContainer;
using VContainer.Unity;

public class EnemyStatus : StatusModelBase, IEnemyStatus
{
    [Tooltip("行動する回数")]
    private int _actionNum = 1;

    [Tooltip("行動パターン")]
    private List<EnemyEffectData> _effectDataList = new List<EnemyEffectData>();
    public IReactiveCollection<EnemyEffectData> EnemyTurnEffect => _enemyTurnEffect;
    [Tooltip("１ターンの間にやる行動")]
    private ReactiveCollection<EnemyEffectData> _enemyTurnEffect = new ReactiveCollection<EnemyEffectData>();

    public override void Init()
    {

    }

    /// <summary>
    /// 階層が進んだことによるStatus補正をかける
    /// </summary>
    /// <param name="enemy"></param>
    public void StatusSet(EnemyStatusData enemy)
    {
        _maxHp.Value = enemy.MaxHp * ((GameManager.CurremtLevel + 1) - enemy.BaseCurrentLevel) * DataBaseScript.EFFECT_MAGNIFICATION;
        _currentHp.Value = _maxHp.Value;
        _effectDataList = new List<EnemyEffectData>(enemy.enemyEffectDataList);
        for (int i = 0; i < _effectDataList.Count; i++) 
        {
            _effectDataList[i].EffectPowerMultiplication(((GameManager.CurremtLevel + 1) - enemy.BaseCurrentLevel) *　DataBaseScript.EFFECT_MAGNIFICATION);
        }

    }

    /// <summary>
    /// このターンの行動を決定する
    /// </summary>
    public void AttackDecision()
    {
        for (int i = 0; i < _actionNum; i++)
        {
            _enemyTurnEffect.Add(_effectDataList[UnityEngine.Random.Range(0, _effectDataList.Count)]);
        }
    }

    public void AttackDecisionReset() 
    {
        for (int i = 0; i < _enemyTurnEffect.Count; i++) 
        {
            _enemyTurnEffect.RemoveAt(0);
        }
    }

    public IStatusBase GetStatusBase()
    {
        return this;
    }

    public IReactiveCollection<EnemyEffectData> GetEnemyTurnEffectOb()
    {
        return _enemyTurnEffect;
    }
}

public struct EnemyStatusData
{
    public string Name;
    public int BaseCurrentLevel;
    public float MaxHp;
    public List<EnemyEffectData> enemyEffectDataList;

    public EnemyStatusData(string name, float maxHp, int baseCurrentLevel, List<EnemyEffectData> effectList)
    {
        enemyEffectDataList = new List<EnemyEffectData>(effectList);
        Name = name;
        MaxHp = maxHp;
        BaseCurrentLevel = baseCurrentLevel;
    }
}

[System.Serializable]
public class EnemyEffectData
{
    public IEffect EnemyEffect => _enemyEffect;
    [SubclassSelector, SerializeReference]
    private IEffect _enemyEffect;

    public TargetType Target => _targetType;
    [SerializeField] private TargetType _targetType;

    public EffectTypeImage ImageType => _imageType;
    [SerializeField] private EffectTypeImage _imageType;

    public float EffectPower => _effectPower;
    [SerializeField] private float _effectPower;
    public EnemyEffectData(IEffect effect, TargetType target, EffectTypeImage image, float power)
    {
        _enemyEffect = effect;
        _targetType = target;
        _imageType = image;
        _effectPower = power;
    }

    public void EffectPowerMultiplication(float value) 
    {
        _effectPower *= value;
    }
}
