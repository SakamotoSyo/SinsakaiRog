using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerEventLifeTimeScope : PlayerLifeTimeScope
{
    [SerializeField] private PlayerEventView _playerEventView;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IPlayerStatus, PlayerStatus>(Lifetime.Singleton);
        builder.RegisterComponent(_playerEventView);
        builder.RegisterEntryPoint<PlayerEventPresenter>(Lifetime.Singleton);
    }
}
