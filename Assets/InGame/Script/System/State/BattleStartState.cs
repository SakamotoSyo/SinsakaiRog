using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;
using System.Threading;

public class BattleStartState : State
{
    protected override void OnEnter(State currentState, CancellationToken token)
    {
        Owner.PlayerController.PlayerStatus.ResetCost();
        Owner.EnemyController.EnemyStatus.AttackDecision();
        Owner.PlayerController.DrawCard(3);
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.BattleStart, token);
    }

    protected override void OnExit(State nextState)
    {
        
    }
}
