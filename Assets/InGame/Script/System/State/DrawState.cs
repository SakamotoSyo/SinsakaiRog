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
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        FieldTest.PlayerReceiveEffect.DrowCard();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        StateMachine.Dispatch((int)BattleStateManager.BattleEvent.PlayerAttack);
    }
}
