using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;

public class BattleStartState : State
{
    protected override void OnEnter(State currentState)
    {
        Owner.PlayerController.PlayerStatus.DeckInit();
        Owner.EnemyController.EnemyStatus.AttackDecision();
        Owner.PlayerController.DrawCard(3);
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.BattleStart);
    }

    protected override void OnExit(State nextState)
    {
        
    }
}
