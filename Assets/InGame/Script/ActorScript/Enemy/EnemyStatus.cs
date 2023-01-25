using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : StatusModelBase
{
    [Tooltip("行動する回数")]
    private int _actionNum = 0;
    public List<EnemyEffectData> EnemyStatusList => _effectDataList;
    private List<EnemyEffectData> _effectDataList = new List<EnemyEffectData>();
    public List<EnemyEffectData> EnemyTurnEffect => _enemyTurnEffect;
    [Tooltip("１ターンの間にやる行動")]
    private List<EnemyEffectData> _enemyTurnEffect = new List<EnemyEffectData>();
    /// <summary>
    /// このターンの行動を決定する
    /// </summary>
    public void AttackDecision() 
    {
        for (int i = 0; i < _actionNum; i++) 
        {
            _enemyTurnEffect.Add(_effectDataList[Random.Range(0, _effectDataList.Count)]);
        }
    }

    /// <summary>
    /// 行動を開始する
    /// </summary>
    /// <param name="playerStatus"></param>
    /// <param name="enemyStatus"></param>
    public void AttackEffect(PlayerStatus playerStatus, EnemyStatus enemyStatus) 
    {
        for (int i = 0; i < _enemyTurnEffect.Count; i++) 
        {
            _enemyTurnEffect[i].EnemyEffect.UseEffect(playerStatus, enemyStatus, _enemyTurnEffect[i].Attack);
        }
    }
}

public struct EnemyEffectData
{
    public IEffect EnemyEffect => _enemyEffect;
    private IEffect _enemyEffect;

    public float Attack => _attack;
    private float _attack;
}
