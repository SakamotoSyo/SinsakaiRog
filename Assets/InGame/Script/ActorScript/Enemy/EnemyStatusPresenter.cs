using System;
using UnityEngine;
using UniRx;
using VContainer.Unity;
using VContainer;

public class EnemyStatusPresenter : IStartable, IDisposable
{
    private EnemyStatusView _enemyView;
    public static IEnemyStatus EnemyStatus => _enemyStatusModel;
    private static IEnemyStatus _enemyStatusModel;
    private CompositeDisposable _compositeDisposable = new();

    [Inject]
    public EnemyStatusPresenter(IEnemyStatus enemyStaus, EnemyStatusView enemyStatusView) 
    {
        _enemyStatusModel = enemyStaus;
        _enemyView = enemyStatusView;
    }

    void IStartable.Start()
    {
        _enemyStatusModel.GetStatusBase().GetMaxHpOb().Subscribe(_enemyView.MaxHpSet).AddTo(_compositeDisposable);
        _enemyStatusModel.GetStatusBase().GetCurrentHpOb().Subscribe(_enemyView.SetHpCurrent).AddTo(_compositeDisposable);
        _enemyStatusModel.GetStatusBase().GetDefeceOb().Subscribe(_enemyView.SetDefense).AddTo(_compositeDisposable);
        _enemyStatusModel.GetEnemyTurnEffectOb().ObserveAdd().Subscribe(x => _enemyView.SetAttackIcon(x.Value)).AddTo(_compositeDisposable);
        _enemyStatusModel.GetEnemyTurnEffectOb().ObserveRemove().Subscribe(x => _enemyView.DeleteIcon(x.Value)).AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}
