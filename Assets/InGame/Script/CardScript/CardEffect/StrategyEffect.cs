using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyEffect : IEffect
{
    public void UseEffect(PlayerController playCon, EnemyController enemyCon, float power, TargetType targetType)
    {
        enemyCon.EnemyStatus.GetEnemyTurnEffect()?.RemoveAt(0);
        AudioManager.Instance.PlaySound(SoundPlayType.SpecialAttack);
    }
}
