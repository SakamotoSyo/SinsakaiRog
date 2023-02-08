using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public IEnemyStatus EnemyStatus => _enemyStatus;

    [SerializeField] private EnemyAnimaiton _enemyAnim = new();
    private EnemyAttack _enemyAttack = new();
    private IEnemyStatus _enemyStatus;
    private IStatusBase _statusBase;

    private void Start()
    {
        _enemyStatus = EnemyStatusPresenter.EnemyStatus;
        _statusBase = _enemyStatus.GetStatusBase();
        _enemyStatus.StatusSet(DataBaseScript.EnemyData[0]);
    }

    /// <summary>
    /// �_���[�W���󂯂��A�̗���
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
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
    /// �U���Ɋւ����A�̗���
    /// </summary>
    public void Attack(PlayerController player)
    {
        //�U���̉񐔕����\�b�h���Ă�
        for (int i = 0; i < _enemyStatus.GetEnemyTurnEffectOb().Count; i++)
        {
            _enemyAttack.AttackEffect(player, this, _enemyStatus.GetEnemyTurnEffectOb()[i]);
            _enemyAnim.AttackAnim();
        }

        _enemyStatus.AttackDecisionReset();
    }
}
