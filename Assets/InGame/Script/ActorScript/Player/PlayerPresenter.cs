using System;
using UniRx;
using VContainer;
using VContainer.Unity;
using Cysharp.Threading.Tasks;

namespace PlayerPresenterName
{
    public class PlayerPresenter : IStartable, IDisposable
    {
        private PlayerView _playerView;
        private IPlayerStatus _playerStauts;
        private CompositeDisposable _compositeDisposable = new();

        [Inject]
        public PlayerPresenter(PlayerView playerView, IPlayerStatus playerStatus, PlayerController playerController)
        {
            _playerView = playerView;
            _playerStauts = playerStatus;
            playerController.SetPlayerStatus(playerStatus);
        }

        public void Start()
        {
            _playerStauts.GetHandCardListOb().ObserveAdd().Subscribe(x => _playerView.DrawView(x.Value)).AddTo(_compositeDisposable);
            _playerStauts.GetGraveyardCardsCountOb().ObserveCountChanged().Subscribe(_playerView.GraveyardCardsView).AddTo(_compositeDisposable);
            _playerStauts.GetDeckCardListOb().ObserveCountChanged().Subscribe(_playerView.DeckCardView).AddTo(_compositeDisposable);
            _playerStauts.GetStatusBase().GetDefeceOb().Subscribe(_playerView.SetDefense).AddTo(_compositeDisposable);
            _playerStauts.GetCostOb().Subscribe(x => _playerView.SetCostText(x).Forget()).AddTo(_compositeDisposable);
            _playerStauts.GetStatusBase().GetMaxHpOb().Subscribe(_playerView.MaxHpSet).AddTo(_compositeDisposable);
            _playerStauts.GetStatusBase().GetCurrentHpOb().Subscribe(_playerView.SetHpCurrent).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}

