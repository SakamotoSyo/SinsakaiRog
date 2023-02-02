using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;

public class EnemyAttackState : State
{

    protected override void OnEnter(State currentState)
    {
        //�G�̍U���s�����n�߂�
        FieldTest.EnemyReceiveEffect.Attack();

    }

    protected override void OnUpdate()
    {
        Debug.Log(StateMachine.CurrentState);
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.Draw);
    }

}
