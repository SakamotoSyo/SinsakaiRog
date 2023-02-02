using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IReceiveEnemyEffect
{
    public EnemyStatus EnemyStatus => _enemyStatus;

    [SerializeField] private EnemyAnimaiton _enemyAnim = new();
    [SerializeField] private EnemyStatus _enemyStatus = new();
    private EnemyAttack _enemyAttack = new();

    private void Awake()
    {
        _enemyStatus.Init();
    }

    /// <summary>
    /// �_���[�W���󂯂��A�̗���
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        _enemyStatus.ChangeValueHealth(_enemyStatus.CurrentHpNum - damage);
        _enemyAnim.DamageAnim();
    }

    /// <summary>
    /// �U���Ɋւ����A�̗���
    /// </summary>
    public void Attack()
    {
        _enemyStatus.AttackDecision(this);

        //�U���̉񐔕����\�b�h���Ă�
        for (int i = 0; i < _enemyStatus.EnemyTurnEffect.Count; i++) 
        {
            _enemyAttack.AttackEffect(_enemyStatus.EnemyTurnEffect[i]);
            _enemyAnim.AttackAnim();
        }
        
    }
}
