using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;

public class PlayerAttackState : State
{
    protected override void OnEnter(State currentState)
    {
        
    }

    protected override void OnExit(State nextState)
    {
        Owner.PlayerController.ResetCost();
    }
}
