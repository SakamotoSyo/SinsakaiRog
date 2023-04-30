using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;
using System.Threading;
public class BattleResultState : State
{
    protected override async void OnEnter(State currentState, CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5), cancellationToken: token);
        Time.timeScale = 1;
        Owner.FieldCs.enabled = false;
        Owner.ResultCanvasManager.ActiveResultPanel();

        //await UniTask.WaitUntil(() => Input.anyKey);
        //GameManager.NextCurrentLevel();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
