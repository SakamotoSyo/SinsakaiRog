using System;
using VContainer;
using VContainer.Unity;
using UniRx;

public class PlayerEventPresenter : IStartable, IDisposable
{
    private PlayerEventView _playerView;
    public static IPlayerStatus PlayerStatus => _playerStatus;
    private static IPlayerStatus _playerStatus;
    private CompositeDisposable _compositeDisposable = new();

    [Inject]
    public PlayerEventPresenter(PlayerEventView playerView, IPlayerStatus playerStatus)
    {
        _playerView = playerView;
        _playerStatus = playerStatus;
    }

    public void Start()
    {
        _playerStatus.GetStatusBase().GetMaxHpOb().Subscribe(_playerView.SetMaxHpView).AddTo(_compositeDisposable);
        _playerStatus.GetStatusBase().GetCurrentHpOb().Subscribe(_playerView.SetCurrentHp).AddTo(_compositeDisposable);
        _playerStatus.GetGoldOb().Subscribe(_playerView.SetGoldView).AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}
