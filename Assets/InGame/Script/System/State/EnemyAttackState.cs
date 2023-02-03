using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;

public class EnemyAttackState : State
{

    protected override void OnEnter(State currentState)
    {
        //�G�̍U���s�����n�߂�
        Owner.EnemyController.Attack(Owner.PlayerController);

    }

    protected override void OnUpdate()
    {
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.Draw);
    }

}
