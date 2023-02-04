using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack
{
    /// <summary>
    /// çsìÆÇäJénÇ∑ÇÈ
    /// </summary>
    /// <param name="playerStatus"></param>
    /// <param name="enemyStatus"></param>
    public void AttackEffect(PlayerController playCon,EnemyController enemyCon, EnemyEffectData enemyTurnEffect)
    {
        Debug.Log(enemyTurnEffect.EffectPower);
        enemyTurnEffect.EnemyEffect.UseEffect(playCon, enemyCon, Mathf.Floor(enemyTurnEffect.EffectPower), enemyTurnEffect.Target);
    }
}
