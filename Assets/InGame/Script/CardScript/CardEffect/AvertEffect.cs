using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvertEffect : IEffect
{
    public void UseEffect(PlayerController playCon, EnemyController enemyCon, float power, TargetType targetType)
    {
        if (TargetType.Enemy == targetType)
        {
            enemyCon.DefenseIncrease(power);
        }
        else if (TargetType.Player == targetType)
        {
            playCon.DefenseIncrease(power);
        }
    }
}
