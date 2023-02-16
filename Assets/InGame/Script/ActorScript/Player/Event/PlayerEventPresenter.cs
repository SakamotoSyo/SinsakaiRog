using System;
using VContainer;
using VContainer.Unity;
using UniRx;

public class PlayerEventPresenter : IStartable, IDisposable
{
    private PlayerEventView _playerView;
    public static IPlayerStatus PlayerStatus => _playerStauts;
    private static IPlayerStatus _playerStauts;
    private CompositeDisposable _compositeDisposable = new();

    [Inject]
    public PlayerEventPresenter(PlayerEventView playerView, IPlayerStatus playerStatus)
    {
        _playerView = playerView;
        _playerStauts = playerStatus;
    }

    public void Start()
    {
        PlayerStatus.GetStatusBase().GetMaxHpOb().Subscribe(_playerView.SetMaxHpView);
        PlayerStatus.GetStatusBase().GetCurrentHpOb().Subscribe(_playerView.SetCurrentHp);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}
