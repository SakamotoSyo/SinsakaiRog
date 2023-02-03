using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack
{
    /// <summary>
    /// �s�����J�n����
    /// </summary>
    /// <param name="playerStatus"></param>
    /// <param name="enemyStatus"></param>
    public void AttackEffect(PlayerController playCon,EnemyController enemyCon, EnemyEffectData enemyTurnEffect)
    {
        enemyTurnEffect.EnemyEffect.UseEffect(playCon, enemyCon, enemyTurnEffect.EffectPower, enemyTurnEffect.Target);
    }
}
