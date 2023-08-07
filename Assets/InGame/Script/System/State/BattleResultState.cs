using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;
using System.Threading;

public class BattleResultState : State
{
    protected override void OnEnter(State currentState, CancellationToken token)
    {
        ResultEnter(token).Forget();
    }

    private async UniTask ResultEnter(CancellationToken token) 
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5), cancellationToken: token);
        Time.timeScale = 1;
        Owner.FieldCs.enabled = false;
        Owner.ResultCanvasManager.ActiveResultPanel();

    }
}
