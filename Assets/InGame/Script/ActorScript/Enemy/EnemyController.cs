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
        _enemyStatus.Init();
    }

    /// <summary>
    /// �_���[�W���󂯂��A�̗���
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        if (0 < _enemyStatus.CurrentHpNum - damage)
        {
            _enemyStatus.ChangeValueHealth(_enemyStatus.CurrentHpNum - damage);
            _enemyAnim.DamageAnim();
        }
        else 
        {
            _enemyAnim.DownAnim();
            
        }
        
    }

    /// <summary>
    /// �U���Ɋւ����A�̗���
    /// </summary>
    public void Attack(PlayerController player)
    {
        _enemyStatus.AttackDecision();

        //�U���̉񐔕����\�b�h���Ă�
        for (int i = 0; i < _enemyStatus.EnemyTurnEffect.Count; i++) 
        {
            _enemyAttack.AttackEffect(player, this, _enemyStatus.EnemyTurnEffect[i]);
            _enemyAnim.AttackAnim();
        }
        
    }
}
