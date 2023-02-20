using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;
using System;
using Cysharp.Threading.Tasks;

public class DrawState : State
{
    protected override async void OnEnter(State currentState)
    {
        Owner.EnemyController.EnemyStatus.AttackDecision();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        Owner.PlayerController.DrawCard();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.PlayerAttack);
    }
}
