using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public Goal.Settings goal;
    public EnemySpawner.Settings enemySpawner;
    public GameInstaller.Settings gameInstaller; // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.BindInstance(goal);
        Container.BindInstance(enemySpawner);
        Container.BindInstance(gameInstaller);
    }
}