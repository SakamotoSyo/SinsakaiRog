using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBreakEffect : IEffect
{
    public void UseEffect(PlayerController playCon, EnemyController enemyCon, float power, TargetType targetType)
    {
        enemyCon.EnemyStatus.GetStatusBase().ChangeValueDefense(0);
    }
}
