using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;
public class BattleResultState : State
{
    protected override async void OnEnter(State currentState)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        Time.timeScale = 1;
        Owner.FieldCs.enabled = false;
        Owner.ResultCanvasManager.ActiveResultPanel();

        //await UniTask.WaitUntil(() => Input.anyKey);
        //GameManager.NextCurrentLevel();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
