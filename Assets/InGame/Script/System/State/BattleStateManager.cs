using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using State = StateMachine<BattleStateManager>.State;

public class BattleStateManager : MonoBehaviour
{
    [SerializeField] private Text _debugText;
    private StateMachine<BattleStateManager> _stateMachine;

    public enum BattleEvent 
    {
        Draw,
        PlayerAttack,
        EnemyAttack,
        BattleResult,
    }

    void Start()
    {
        _stateMachine = new StateMachine<BattleStateManager>(this);
        _stateMachine.AddTransition<DrawState, PlayerAttackState>((int)BattleEvent.PlayerAttack);
        _stateMachine.AddTransition<PlayerAttackState, EnemyAttackState>((int)BattleEvent.EnemyAttack);
        _stateMachine.AddTransition<EnemyAttackState, DrawState>((int)BattleEvent.Draw);
        _stateMachine.AddAnyTranstion<BattleResultState>((int)BattleEvent.BattleResult);
        //��ԍŏ��̃X�e�[�g���n�߂�
        _stateMachine.Start<DrawState>();
    }

    void Update()
    {
        _stateMachine.Update();
        _debugText.text = _stateMachine.CurrentState.ToString();
    }

    /// <summary>
    ///�G�̍U���^�[���ɑJ�ڂ�����
    /// �{�^���ŌĂяo�������B
    /// </summary>
    public void BattleTrunEnd() 
    {
       if(_stateMachine.CurrentState == _stateMachine.GetOrAddState<PlayerAttackState>()) 
        {
            _stateMachine.Dispatch((int)BattleEvent.EnemyAttack);
        }
    }
}
