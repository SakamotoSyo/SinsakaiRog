using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

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

    public async UniTask AttackAnim(CancellationToken token)
    {
        _anim.SetTrigger(_attackParm);
        
        await UniTask.WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f, 
                                cancellationToken: token);
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
