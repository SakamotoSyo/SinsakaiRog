using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

public class BattleStateManager : MonoBehaviour
{
    public FieldTest FieldCs => _fieldCs;
    public ResultCanvasManager ResultCanvasManager => _resultManager;
    public PlayerController PlayerController => _playerCon;
    public EnemyController EnemyController => _enemyCon;
    public BattleStateView BattleStateView => _battleStateView;
    public Animator GameOverAnim => _gameOverAnim;
    public GameObject GameOverObj => _gameOverObj;

    [SerializeField] private GameObject _gameOverObj;
    [SerializeField] private Animator _gameOverAnim;
    [SerializeField] private BattleStateView _battleStateView;
    [SerializeField] private ActorGenerator _generator;
    [SerializeField] private ResultCanvasManager _resultManager;
    [SerializeField] private FieldTest _fieldCs;
    private bool _isStart = false;
    private StateMachine<BattleStateManager> _stateMachine;
    private PlayerController _playerCon;
    private CancellationTokenSource _cancellationToken;
    private EnemyController _enemyCon;

    public enum BattleEvent
    {
        BattleStart,
        Draw,
        PlayerAttack,
        EnemyAttack,
        BattleResult,
        GameOver,
    }

    private async void Start()
    {
        _cancellationToken = new CancellationTokenSource();
        await FadeScript.Instance.FadeIn();
        _isStart = true;
        _playerCon = _generator.PlayerController;
        _enemyCon = _generator.EnemyController;
        _stateMachine = new StateMachine<BattleStateManager>(this);
        _stateMachine.AddTransition<BattleStartState, PlayerAttackState>((int)BattleEvent.BattleStart);
        _stateMachine.AddTransition<DrawState, PlayerAttackState>((int)BattleEvent.PlayerAttack);
        _stateMachine.AddTransition<PlayerAttackState, EnemyAttackState>((int)BattleEvent.EnemyAttack);
        _stateMachine.AddTransition<EnemyAttackState, DrawState>((int)BattleEvent.Draw);
        _stateMachine.AddAnyTranstion<BattleResultState>((int)BattleEvent.BattleResult);
        _stateMachine.AddAnyTranstion<GameOverState>((int)BattleEvent.GameOver);
        //一番最初のステートを始める
        _stateMachine.Start<BattleStartState>(_cancellationToken.Token);
        _enemyCon.EnemyStatus.GetStatusBase().GetCurrentHpOb().Subscribe(ToBattleResult).AddTo(this);
        _playerCon.PlayerStatus.GetStatusBase().GetCurrentHpOb().Subscribe(ToGameOverState).AddTo(this);
    }

    void Update()
    {
        if (_isStart)
        {
            _stateMachine.Update();
            _battleStateView.DebugTurnText(_stateMachine.CurrentState.ToString());
        }
    }

    public void ToEnemyTrun()
    {
        _stateMachine.Dispatch((int)BattleEvent.EnemyAttack, _cancellationToken.Token);
    }

    public void ToBattleResult(float value)
    {
        if (value <= 0)
        {
            Time.timeScale = 0.3f;
            _stateMachine.Dispatch((int)BattleEvent.BattleResult, _cancellationToken.Token);
        }
    }

    public void ToGameOverState(float value) 
    {
        if(value <= 0) 
        {
            Time.timeScale = 0.3f;
            _stateMachine.Dispatch((int)BattleEvent.GameOver, _cancellationToken.Token);
        }
    }

    private void OnDestroy()
    {
        _cancellationToken.Cancel();   
    }
}
