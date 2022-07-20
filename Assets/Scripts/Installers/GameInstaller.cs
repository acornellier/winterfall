using System;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    private readonly Settings _settings;

    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Spawn>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Goal>().FromComponentInHierarchy().AsSingle();
        
        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.BindFactory<UnityEngine.Object, Enemy.Stats, Enemy, Enemy.Factory>()
            .FromFactory<PrefabFactory<Enemy.Stats, Enemy>>();
    }

    [Serializable]
    public class Settings
    {
    }
}