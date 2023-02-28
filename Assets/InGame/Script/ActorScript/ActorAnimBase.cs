using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ActorAnimBase
{
    [AnimationParameter]
    [SerializeField] private string _downParm;
    [AnimationParameter]
    [SerializeField] private string _attackParm;
    [AnimationParameter]
    [SerializeField] private string _downAnim;
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _defenceAnim;

    public virtual void DamageAnim()
    {
        _anim.SetTrigger(_downParm);
    }

    public async UniTask AttackAnim()
    {
        _anim.SetTrigger(_attackParm);
        await UniTask.WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
    }

    public void DownAnim()
    {
        _anim.SetTrigger(_downAnim);
    }

    public virtual void ActiveDefence() 
    {
        _defenceAnim.SetTrigger("Active");
    }

    public virtual void LostDefence() 
    {
        _defenceAnim.SetTrigger("Lost");
    }
}
