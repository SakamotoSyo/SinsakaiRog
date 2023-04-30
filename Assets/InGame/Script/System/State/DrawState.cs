using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class DrawState : State
{
    protected override async void OnEnter(State currentState, CancellationToken token)
    {
        Owner.EnemyController.EnemyStatus.AttackDecision();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
        Owner.PlayerController.DrawCard(3);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.PlayerAttack, token);
    }
}
