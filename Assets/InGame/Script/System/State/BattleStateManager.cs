using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using State = StateMachine<BattleStateManager>.State;

public class BattleStateManager : MonoBehaviour
{
    public PlayerController PlayerController => _playerCon;
    public EnemyController EnemyController => _enemyCon;
    public BattleStateView BattleStateView => _battleStateView;

    [SerializeField] private BattleStateView _battleStateView;
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
        _battleStateView.DebugTurnText(_stateMachine.CurrentState.ToString());
    }

    
    public void BattleTrunEnd() 
    {
       if(_stateMachine.CurrentState == _stateMachine.GetOrAddState<PlayerAttackState>()) 
        {
            _stateMachine.Dispatch((int)BattleEvent.EnemyAttack);
        }
    }

    public void ToBattleResult(float value) 
    {
        if (value <= 0) 
        {
            _stateMachine.Dispatch((int)BattleEvent.BattleResult);
        }
    }
}
