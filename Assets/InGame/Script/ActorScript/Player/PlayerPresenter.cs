using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private PlayerView _playerView;
    private PlayerStatus _playerStauts;

    void Start()
    {
        _playerStauts = _controller.PlayerStatus;
        _playerStauts.HandCardList.ObserveAdd().Subscribe(x => _playerView.DrawView(x.Value)).AddTo(this);
        _playerStauts.DiscardedCount.ObserveCountChanged().Subscribe(_playerView.DiscardedCardView).AddTo(this);
        _playerStauts.DeckCardList.ObserveCountChanged().Subscribe(_playerView.DeckCardView).AddTo(this);
        _playerStauts.Defense.Subscribe(_playerView.SetDefense).AddTo(this);
        _playerStauts.CostOb.Subscribe(_playerView.SetCost).AddTo(this);
        _playerStauts.MaxHp.Subscribe(_playerView.MaxHpSet).AddTo(this);
        _playerStauts.CurrentHp.Subscribe(_playerView.SetHpCurrent).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
