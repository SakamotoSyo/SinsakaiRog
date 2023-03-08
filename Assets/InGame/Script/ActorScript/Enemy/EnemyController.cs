using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemyController : MonoBehaviour
{
    public IEnemyStatus EnemyStatus => _enemyStatus;

    [SerializeField] private EnemyAnimaiton _enemyAnim = new();
    private EnemyAttack _enemyAttack = new();
    private IEnemyStatus _enemyStatus;
    private IStatusBase _statusBase;

    private void Start()
    {
        _statusBase = _enemyStatus.GetStatusBase();
    }

    /// <summary>
    /// ダメージを受ける一連の流れ
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        AudioManager.Instance.PlaySound(SoundPlayType.PlayerAttack);
        if (_statusBase.DownJudge(damage))
        {
            _statusBase.AddDamage(damage);
            _enemyAnim.DamageAnim();
        }
        else
        {
            _statusBase.AddDamage(damage);
            _enemyAnim.DownAnim();
        }
    }

    public void DefenseIncrease(float num)
    {
        _enemyAnim.ActiveDefence();
        _statusBase.DefenseIncrease(num);
    }

    /// <summary>
    /// 攻撃に関する一連の流れ
    /// </summary>
    public async UniTask Attack(PlayerController player)
    {
        var loopNum = _enemyStatus.GetEnemyTurnEffectOb().Count;
        //攻撃の回数分メソッドを呼ぶ
        for (int i = 0; i < loopNum; i++)
        {
            Debug.Log(_enemyStatus.GetEnemyTurnEffectOb().Count);
            _enemyAttack.AttackEffect(player, this, _enemyStatus.GetEnemyTurnEffectOb()[0]);
            _enemyStatus.AttackDecisionReset();
            await _enemyAnim.AttackAnim();
        }
    }

    public void SetEnemyStatus(IEnemyStatus enemyStatus) 
    {
        _enemyStatus = enemyStatus;
    }
}
