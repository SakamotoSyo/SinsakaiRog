using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using VContainer;
using VContainer.Unity;

public class EnemyStatus : StatusModelBase, IEnemyStatus
{
    [Tooltip("行動する回数")]
    private string _actionNum;
    private EnemyStatusData _enemyStatus;
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
        _maxHp.Value = Mathf.Floor(enemy.MaxHp * EffectMagnifivationNum(enemy.BaseCurrentLevel));
        _currentHp.Value = _maxHp.Value;
        _actionNum = enemy.ActionNum;
        _effectDataList = new List<EnemyEffectData>(enemy.enemyEffectDataList);
        for (int i = 0; i < _effectDataList.Count; i++) 
        {
            _effectDataList[i].EffectPowerMultiplication(EffectMagnifivationNum(enemy.BaseCurrentLevel));
        }
    }

    /// <summary>
    /// このターンの行動を決定する
    /// </summary>
    public void AttackDecision()
    {
        var actionNum = 0;
        if (_actionNum.Contains("~"))
        {
            var numArray = _actionNum.Split('~');
            actionNum = UnityEngine.Random.Range(int.Parse(numArray[0]), int.Parse(numArray[1]) + 1);
        }
        else 
        {
            actionNum = int.Parse(_actionNum);
        }

        for (int i = 0; i < actionNum; i++)
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

    /// <summary>
    /// 階層にあったStatusを上昇させる倍率を返してくれる
    /// </summary>
    /// <param name="BaseLevel"></param>
    /// <returns></returns>
    public float EffectMagnifivationNum(int BaseLevel) 
    {
        return 1 + ((GameManager.CurremtLevel + 1) - BaseLevel) * DataBaseScript.EFFECT_MAGNIFICATION;
    }

    public ReactiveCollection<EnemyEffectData> GetEnemyTurnEffect()
    {
        return _enemyTurnEffect;
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
