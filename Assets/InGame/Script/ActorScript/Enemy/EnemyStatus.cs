using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using VContainer;
using VContainer.Unity;

public class EnemyStatus : StatusModelBase, IEnemyStatus
{
    [Tooltip("�s�������")]
    private int _actionNum = 1;

    [Tooltip("�s���p�^�[��")]
    private List<EnemyEffectData> _effectDataList = new List<EnemyEffectData>();
    public IReactiveCollection<EnemyEffectData> EnemyTurnEffect => _enemyTurnEffect;
    [Tooltip("�P�^�[���̊Ԃɂ��s��")]
    private ReactiveCollection<EnemyEffectData> _enemyTurnEffect = new ReactiveCollection<EnemyEffectData>();

    public override void Init()
    {

    }

    /// <summary>
    /// �K�w���i�񂾂��Ƃɂ��Status�␳��������
    /// </summary>
    /// <param name="enemy"></param>
    public void StatusSet(EnemyStatusData enemy)
    {
        _maxHp.Value = Mathf.Floor(enemy.MaxHp * EffectMagnifivationNum(enemy.BaseCurrentLevel));
        _currentHp.Value = _maxHp.Value;
        _effectDataList = new List<EnemyEffectData>(enemy.enemyEffectDataList);
        for (int i = 0; i < _effectDataList.Count; i++) 
        {
            _effectDataList[i].EffectPowerMultiplication(EffectMagnifivationNum(enemy.BaseCurrentLevel));
        }

    }

    /// <summary>
    /// ���̃^�[���̍s�������肷��
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

    /// <summary>
    /// �K�w�ɂ�����Status���㏸������{����Ԃ��Ă����
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
