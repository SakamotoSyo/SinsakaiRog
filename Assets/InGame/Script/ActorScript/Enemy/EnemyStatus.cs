using System.Collections;
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
    [SerializeField]private List<EnemyEffectData> _effectDataList = new List<EnemyEffectData>();
    public List<EnemyEffectData> EnemyTurnEffect => _enemyTurnEffect;
    [Tooltip("�P�^�[���̊Ԃɂ��s��")]
    private List<EnemyEffectData> _enemyTurnEffect = new List<EnemyEffectData>();
    /// <summary>
    /// ���̃^�[���̍s�������肷��
    /// </summary>
    public void AttackDecision(IReceiveEnemyEffect enemyEffect) 
    {
        for (int i = 0; i < _actionNum; i++) 
        {
            _enemyTurnEffect.Add(_effectDataList[UnityEngine.Random.Range(0, _effectDataList.Count)]);
        }
    }
}

[System.Serializable]
public struct EnemyEffectData
{
    public IEffect EnemyEffect => _enemyEffect;
    [SubclassSelector, SerializeReference]
    private IEffect _enemyEffect;

    public TargetType Target => _targetType;
    [SerializeField]private TargetType _targetType;

    public float EffectPower => _effectPower;
    [SerializeField]private float _effectPower;
}
