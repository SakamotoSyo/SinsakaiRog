using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifeTimeScope : LifetimeScope
{
    [SerializeField] private EnemyStatusView _enemyStatusView;
    [SerializeField] private EnemyController _enemyController;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IEnemyStatus, EnemyStatus>(Lifetime.Singleton);
        builder.RegisterComponent(_enemyStatusView);
        builder.RegisterComponent(_enemyController);
        builder.RegisterEntryPoint<EnemyStatusPresenter>(Lifetime.Singleton);
    }
}
