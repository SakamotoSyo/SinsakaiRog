using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEffect : IEffect
{
    public void UseEffect(PlayerController playCon, EnemyController enemyCon, float power, TargetType targetType)
    {
        playCon.DrawCard(power);
    }
}
