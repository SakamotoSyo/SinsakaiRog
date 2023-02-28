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
        Owner.PlayerController.PlayerTurnEnd();
        Owner.BattleStateView.TurnAnim(this);
        await UniTask.Delay(TimeSpan.FromSeconds(_trunAnimTime));
        //ìGÇÃçUåÇçsìÆÇénÇﬂÇÈ
        await Owner.EnemyController.Attack(Owner.PlayerController);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.Draw);
    }

    protected override void OnUpdate()
    {
       
    }

}
