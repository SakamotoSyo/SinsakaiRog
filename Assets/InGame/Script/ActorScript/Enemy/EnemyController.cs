using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStatus EnemyStatus => _enemyStatus;

    [SerializeField] private EnemyAnimaiton _enemyAnim = new();
    [SerializeField] private EnemyStatus _enemyStatus = new();
    private EnemyAttack _enemyAttack = new();

    private void Awake()
    {
        
    }

    /// <summary>
    /// �_���[�W���󂯂��A�̗���
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        if (0 < _enemyStatus.CurrentHpNum + _enemyStatus.DefenceNum - damage)
        {
            _enemyStatus.AddDamage(damage);
            _enemyAnim.DamageAnim();
        }
        else
        {
            _enemyStatus.ChangeValueHealth(0);
            _enemyAnim.DownAnim();
        }
    }

    public void DefenseIncrease(float num)
    {
        _enemyAnim.ActiveDefence();
        _enemyStatus.DefenseIncrease(num);
    }

    /// <summary>
    /// �U���Ɋւ����A�̗���
    /// </summary>
    public void Attack(PlayerController player)
    {
        //�U���̉񐔕����\�b�h���Ă�
        for (int i = 0; i < _enemyStatus.EnemyTurnEffect.Count; i++)
        {
            _enemyAttack.AttackEffect(player, this, _enemyStatus.EnemyTurnEffect[i]);
            _enemyAnim.AttackAnim();
        }

        _enemyStatus.AttackDecisionReset();
    }
}
