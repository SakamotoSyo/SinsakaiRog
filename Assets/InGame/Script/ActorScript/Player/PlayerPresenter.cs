using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] PlayerStatus _playerStauts;
    [SerializeField] PlayerView _playerView;
    // Start is called before the first frame update
    void Start()
    {
        _playerStauts.HandCardList.ObserveAdd().Subscribe(x => _playerView.DrawView(x.Value)).AddTo(this);
        _playerStauts.MaxHp.Subscribe(value => _playerView.MaxHpSet(value)).AddTo(this);
        _playerStauts.CurrentHp.Subscribe(value => _playerView.SetHpCurrent(value)).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
