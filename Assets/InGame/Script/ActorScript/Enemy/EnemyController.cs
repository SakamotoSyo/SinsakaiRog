using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public IEnemyStatus EnemyStatus => _enemyStatus;

    [SerializeField] private EnemyAnimaiton _enemyAnim = new();
    private EnemyAttack _enemyAttack = new();
    private CancellationTokenSource _cancellationToken;
    private IEnemyStatus _enemyStatus;
    private IStatusBase _statusBase;

    private void Start()
    {
        _cancellationToken = new CancellationTokenSource();
        _statusBase = _enemyStatus.GetStatusBase();
    }

    /// <summary>
    /// �_���[�W���󂯂��A�̗���
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
    /// �U���Ɋւ����A�̗���
    /// </summary>
    public async UniTask Attack(PlayerController player)
    {
        var loopNum = _enemyStatus.GetEnemyTurnEffectOb().Count;
        //�U���̉񐔕����\�b�h���Ă�
        for (int i = 0; i < loopNum; i++)
        {
            Debug.Log(_enemyStatus.GetEnemyTurnEffectOb().Count);
            _enemyAttack.AttackEffect(player, this, _enemyStatus.GetEnemyTurnEffectOb()[0]);
            _enemyStatus.AttackDecisionReset();
            await _enemyAnim.AttackAnim(_cancellationToken.Token);
        }
    }

    public void SetEnemyStatus(IEnemyStatus enemyStatus) 
    {
        _enemyStatus = enemyStatus;
    }
}
