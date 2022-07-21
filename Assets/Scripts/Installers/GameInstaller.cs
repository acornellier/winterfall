using System;
using Zenject;
using Object = UnityEngine.Object;

public class GameInstaller : MonoInstaller
{
    [Inject] readonly Settings _settings; // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<Spawn>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Goal>().FromComponentInHierarchy().AsSingle();

        Container.Bind<TowerPlacer>().AsSingle();

        // Container.Bind<TowerStatePlacing>().AsSingle();

        Container.BindFactory<Tower, Tower, Tower.Factory>()
            .FromFactory<PrefabFactory<Tower>>();

        Container.BindFactory<Tower, TowerStatePlacing, TowerStatePlacing.Factory>();
        Container.BindFactory<Tower, TowerStateIdle, TowerStateIdle.Factory>();

        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.BindFactory<Object, Enemy.Stats, Enemy, Enemy.Factory>()
            .FromFactory<PrefabFactory<Enemy.Stats, Enemy>>();
    }

    [Serializable]
    public class Settings
    {
    }
}