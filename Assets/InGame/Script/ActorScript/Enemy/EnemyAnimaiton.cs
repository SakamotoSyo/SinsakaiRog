using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyAnimaiton : ActorAnimBase
{
    [SerializeField] private Animator _effectAnim;


    public override void DamageAnim()
    {
        base.DamageAnim();
        _effectAnim.SetTrigger("AttackEffect");
    }
}
