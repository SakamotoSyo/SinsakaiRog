using UnityEngine;
using System;

[Serializable]
public class PlayerAnim
{
    [AnimationParameter]
    [SerializeField] private string _downParm;
    [AnimationParameter]
    [SerializeField] private string _attackParm;
    [SerializeField] private Animator _playerAnim;

    public void DamageAnim() 
    {
        _playerAnim.SetTrigger(_downParm);
    }

    public void AttackAnim() 
    {
        _playerAnim.SetTrigger(_attackParm);
    }

    public void DownAnim() 
    {

    }
}
