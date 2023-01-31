using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifeTimeScope : LifetimeScope
{
    [SerializeField] private PlayerView _playerView;
    //private IObjectResolver _resolver;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerPresenter>(Lifetime.Singleton);
        builder.Register<IPlayerStatus, PlayerStatus>(Lifetime.Singleton);
        builder.RegisterComponent(_playerView);

        //_resolver = builder.Build();
    }

    ///// <summary>
    ///// 生成したインターフェースを返す
    ///// </summary>
    ///// <returns></returns>
    //public IPlayerStatus GetPlayerStatus() 
    //{
    //    return _resolver.Resolve<IPlayerStatus>();
    //}
}
