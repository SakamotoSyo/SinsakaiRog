using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using State = StateMachine<BattleStateManager>.State;

public class PlayerAttackState : State
{
    private readonly float _trunAnimTime = 2;
    protected override async void OnEnter(State currentState)
    {
        Owner.BattleStateView.TurnAnim(this);
        await UniTask.Delay(TimeSpan.FromSeconds(_trunAnimTime));
        Owner.BattleStateView.StopButton(false);
    }

    protected override void OnExit(State nextState)
    {
        Owner.PlayerController.ResetCost();
        Owner.BattleStateView.StopButton(true);
    }
}
