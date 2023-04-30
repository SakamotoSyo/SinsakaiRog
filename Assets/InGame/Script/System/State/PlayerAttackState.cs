using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using State = StateMachine<BattleStateManager>.State;
using System.Threading;
public class PlayerAttackState : State
{
    private readonly float _trunAnimTime = 2;
    protected override async void OnEnter(State currentState, CancellationToken token)
    {
        AudioManager.Instance.PlaySound(SoundPlayType.TurnSwitching);
        Owner.BattleStateView.TurnAnim(this);
        await UniTask.Delay(TimeSpan.FromSeconds(_trunAnimTime), cancellationToken:token);
        Owner.BattleStateView.StopButton(false);
    }

    protected override void OnExit(State nextState)
    {
        Owner.PlayerController.ResetCost();
        Owner.BattleStateView.StopButton(true);
    }
}
