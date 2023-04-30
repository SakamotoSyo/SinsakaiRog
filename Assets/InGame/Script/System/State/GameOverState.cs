using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;
using Cysharp.Threading.Tasks;
using System.Threading;

public class GameOverState : State
{
    protected override async void OnEnter(State currentState, CancellationToken token)
    {
        
        Owner.GameOverObj.SetActive(true);
        Owner.GameOverAnim.SetTrigger("TurnAnim");
        await UniTask.WaitUntil(() => Owner.GameOverAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f, 
                                 cancellationToken: token);
        await FadeScript.Instance.FadeOut();
        Time.timeScale = 1f;
        LoadSceneManager.ToTitleScene();
    }
}
