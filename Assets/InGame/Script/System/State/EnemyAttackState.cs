using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using State = StateMachine<BattleStateManager>.State;

public class EnemyAttackState : State
{
    private readonly float _trunAnimTime = 2;
    protected async override void OnEnter(State currentState, CancellationToken token)
    {
        AudioManager.Instance.PlaySound(SoundPlayType.TurnSwitching);
        Owner.PlayerController.PlayerTurnEnd();
        Owner.BattleStateView.TurnAnim(this);
        await UniTask.Delay(TimeSpan.FromSeconds(_trunAnimTime), cancellationToken: token);
        //“G‚ÌUŒ‚s“®‚ðŽn‚ß‚é
        await Owner.EnemyController.Attack(Owner.PlayerController);
        if (Owner.PlayerController.PlayerStatus.GetStatusBase().GetCurrentHpNum() > 0) 
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5), cancellationToken: token);
            StateMachine.Dispatch((int)BattleStateManager.BattleEvent.Draw, token);
        }
    }
}
