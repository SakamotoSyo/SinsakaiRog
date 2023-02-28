using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeEffect : IEffect
{
    public void UseEffect(PlayerController playCon, EnemyController enemyCon, float power, TargetType targetType)
    {
        enemyCon.AddDamage(power);
        enemyCon.EnemyStatus.GetStatusBase().ChangeValueDefense(0);
    }
}
