using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using State = StateMachine<BattleStateManager>.State;

public class BattleStateManager : MonoBehaviour
{
    public PlayerController PlayerController => _playerCon;
    public EnemyController EnemyController => _enemyCon;

    [SerializeField] private Text _debugText;
    [SerializeField] private ActorGenerator _generator;
    private StateMachine<BattleStateManager> _stateMachine;
    private PlayerController _playerCon;
    private EnemyController _enemyCon;

    public enum BattleEvent 
    {
        Draw,
        PlayerAttack,
        EnemyAttack,
        BattleResult,
    }

    void Start()
    {
        _playerCon = _generator.PlayerController;
        _enemyCon = _generator.EnemyController;
        _stateMachine = new StateMachine<BattleStateManager>(this);
        _stateMachine.AddTransition<DrawState, PlayerAttackState>((int)BattleEvent.PlayerAttack);
        _stateMachine.AddTransition<PlayerAttackState, EnemyAttackState>((int)BattleEvent.EnemyAttack);
        _stateMachine.AddTransition<EnemyAttackState, DrawState>((int)BattleEvent.Draw);
        _stateMachine.AddAnyTranstion<BattleResultState>((int)BattleEvent.BattleResult);
        //一番最初のステートを始める
        _stateMachine.Start<DrawState>();
        _enemyCon.EnemyStatus.CurrentHp.Subscribe(ToBattleResult).AddTo(this);
    }

    void Update()
    {
        _stateMachine.Update();
        _debugText.text = _stateMachine.CurrentState.ToString();
    }

    /// <summary>
    ///敵の攻撃ターンに遷移させる
    /// ボタンで呼び出す函数。
    /// </summary>
    public void BattleTrunEnd() 
    {
       if(_stateMachine.CurrentState == _stateMachine.GetOrAddState<PlayerAttackState>()) 
        {
            _stateMachine.Dispatch((int)BattleEvent.EnemyAttack);
        }
    }

    public void ToBattleResult(float value) 
    {
        
    }
}
