using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack
{
    /// <summary>
    /// 行動を開始する
    /// </summary>
    /// <param name="playerStatus"></param>
    /// <param name="enemyStatus"></param>
    public void AttackEffect(PlayerController playCon,EnemyController enemyCon, EnemyEffectData enemyTurnEffect)
    {
        enemyTurnEffect.EnemyEffect.UseEffect(playCon, enemyCon, Mathf.Floor(enemyTurnEffect.EffectPower), enemyTurnEffect.Target);
    }
}
