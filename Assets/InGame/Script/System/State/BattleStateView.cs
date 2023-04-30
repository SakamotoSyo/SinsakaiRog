using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using State = StateMachine<BattleStateManager>.State;

[Serializable]
public class BattleStateView
{
    [SerializeField] private Text _debugText;
    [SerializeField] private Text _trunText;
    [SerializeField] private GameObject _stopButtonObj;
    [SerializeField] private Animator _turnAnim;

    public void DebugTurnText(string st) 
    {
        _debugText.text = st;
    }

    public void TurnAnim(State state)
    {
        if (state.ToString() == "PlayerAttackState")
        {
            _trunText.text = "�v���C���[�̃^�[��";
            _turnAnim.SetTrigger("TurnAnim");
        }
        else if (state.ToString() == "EnemyAttackState")
        {
            _trunText.text = "�G�l�~�[�̃^�[��";
            _turnAnim.SetTrigger("TurnAnim");
        }
    }

    /// <summary>
    /// �{�^�����~�����邩�ǂ���
    /// </summary>
    /// <param name="b"></param>
    public void StopButton(bool b)
    {
        _stopButtonObj.SetActive(b);
    }
}
