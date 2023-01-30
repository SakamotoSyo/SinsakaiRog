using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusView : ActorViewBase
{
    [SerializeField] private EnemyAnimaiton _enemyAnimaiton;

    public override void SetHpCurrent(float currentHp)
    {
        base.SetHpCurrent(currentHp);
        _enemyAnimaiton.DamageAnim();
    }
}
