using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using PlayerPresenterName;

public class PlayerLifeTimeScope : LifetimeScope
{
    [SerializeField] private PlayerView _playerView;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IPlayerStatus, PlayerStatus>(Lifetime.Singleton);
        builder.RegisterComponent(_playerView);
        builder.RegisterEntryPoint<PlayerPresenter>(Lifetime.Singleton);
    }

}
