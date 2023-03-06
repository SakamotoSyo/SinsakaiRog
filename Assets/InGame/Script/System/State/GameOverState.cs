using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleStateManager>.State;
using Cysharp.Threading.Tasks;

public class GameOverState : State
{
    protected override async void OnEnter(State currentState)
    {
        Owner.GameOverObj.SetActive(true);
        Owner.GameOverAnim.SetTrigger("TurnAnim");
        await UniTask.WaitUntil(() => Owner.GameOverAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f);
        await FadeScript.Instance.FadeOut();
        Time.timeScale = 1f;
        LoadSceneManager.ToTitleScene();
    }
}
