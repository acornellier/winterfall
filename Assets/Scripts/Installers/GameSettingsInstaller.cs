using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public Player.Settings player;
    public EnemySpawner.Settings enemySpawner;
    public GameInstaller.Settings gameInstaller;

    public override void InstallBindings()
    {
        Container.BindInstance(player);
        Container.BindInstance(enemySpawner);
        Container.BindInstance(gameInstaller);
    }
}