using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEffect : IEffect
{
    public void UseEffect(PlayerController playCon, EnemyController enemyCon, float power, TargetType targetType)
    {
        playCon.UseCost(power * -1);
    }
}
