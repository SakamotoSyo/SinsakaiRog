using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifeTimeScope : LifetimeScope
{
    [SerializeField] private EnemyStatusView _enemyStatusView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IEnemyStatus, EnemyStatus>(Lifetime.Singleton);
        builder.RegisterComponent(_enemyStatusView);
        builder.RegisterEntryPoint<EnemyStatusPresenter>(Lifetime.Singleton);
    }
}
