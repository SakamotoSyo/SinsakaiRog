using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : IEffect
{
    [SerializeField] private float _power;
    public void UseEffect(PlayerController playCon, EnemyController enemyCon, float power, TargetType targetType)
    {
        if (TargetType.Enemy == targetType)
        {
            enemyCon.AddDamage(power);
        }
        else if (TargetType.Player == targetType)
        {
            playCon.AddDamage(power);
        }
    }
}
