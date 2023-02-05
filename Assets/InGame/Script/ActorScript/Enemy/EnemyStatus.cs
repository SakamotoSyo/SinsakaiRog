using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyStatus : StatusModelBase
{
    [Tooltip("�s�������")]
    private int _actionNum = 1;

    //TODO: �f�[�^�̒������@���l���錻�󂾂�SerializeField
    [Tooltip("�s���p�^�[��")]
    public List<EnemyEffectData> EnemyStatusList => _effectDataList;
    [SerializeField] private List<EnemyEffectData> _effectDataList = new List<EnemyEffectData>();
    public List<EnemyEffectData> EnemyTurnEffect => _enemyTurnEffect;
    [Tooltip("�P�^�[���̊Ԃɂ��s��")]
    private List<EnemyEffectData> _enemyTurnEffect = new List<EnemyEffectData>();

    public override void Init()
    {

    }

    /// <summary>
    /// �G�Ɋւ���Status�̏��������ŃZ�b�g����
    /// </summary>
    /// <param name="enemy"></param>
    public void StatusSet(EnemyStatusData enemy)
    {
        _maxHp.Value = enemy.MaxHp * ((GameManager.CurremtLevel + 1) - enemy.BaseCurrentLevel) * DataBaseScript.EFFECT_MAGNIFICATION;
        _currentHp.Value = _maxHp.Value;
        _effectDataList = new List<EnemyEffectData>(enemy.enemyEffectDataList);
        for (int i = 0; i < _effectDataList.Count; i++) 
        {
            _effectDataList[i].EffectPowerMultiplication(((GameManager.CurremtLevel + 1) - enemy.BaseCurrentLevel) *�@DataBaseScript.EFFECT_MAGNIFICATION);
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
        _enemyTurnEffect.Clear();
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

    public float EffectPower => _effectPower;
    [SerializeField] private float _effectPower;
    public EnemyEffectData(IEffect effect, TargetType target, float power)
    {
        _enemyEffect = effect;
        _targetType = target;
        _effectPower = power;
    }

    public void EffectPowerMultiplication(float value) 
    {
        _effectPower *= value;
        Debug.Log(_effectPower);
    }
}
