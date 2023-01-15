using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<BattleTurnManager>.State;

public class BattleTurnManager : MonoBehaviour
{
    private StateMachine<BattleTurnManager> stateMachine;

    public enum TurnEvent
    {
        DrawPhase,
        PlayerPhase,
        EnemyPhase,
        EndPhase,
    }

    private void Start()
    {
        stateMachine = new StateMachine<BattleTurnManager>(this);
        stateMachine.AddTransition<DrawTurnScript, PlayerTurnScript>((int)TurnEvent.DrawPhase);
    }
}