using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using State = StateMachine<BattleStateManager>.State;

public class EnemyAttackState : State
{
    private readonly float _trunAnimTime = 2;
    protected async override void OnEnter(State currentState)
    {
        Owner.BattleStateView.TurnAnim(this);
        await UniTask.Delay(TimeSpan.FromSeconds(_trunAnimTime));
        //�G�̍U���s�����n�߂�
        Owner.EnemyController.Attack(Owner.PlayerController);
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.Draw);
        //TODO:�}�W�b�N�i���o�[��߂�
        await UniTask.Delay(TimeSpan.FromSeconds(1));
    }

    protected override void OnUpdate()
    {
       
    }

}
