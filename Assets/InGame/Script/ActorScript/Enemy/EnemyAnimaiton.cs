using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyAnimaiton
{
    [AnimationParameter]
    [SerializeField] private string _damageParm; 
    [AnimationParameter]
    [SerializeField] private string _attackParm;
    [SerializeField] private Animator _enemyAnim;
    [SerializeField] private Animator _effectAnim;

    /// <summary>
    /// �_���[�WAnimation���X�^�[�g����
    /// </summary>
    public void DamageAnim() 
    {
        _enemyAnim.SetTrigger(_damageParm);
        _effectAnim.SetTrigger("AttackEffect");
    }

    public void AttackAnim() 
    {
        _enemyAnim.SetTrigger(_attackParm);
    }

    public void DownAnim() 
    {

    }
}
