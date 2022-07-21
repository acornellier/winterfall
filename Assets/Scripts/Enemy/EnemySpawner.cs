using System;
using UnityEngine;
using Zenject;

public class EnemySpawner : IInitializable, ITickable
{
    [Inject] readonly Enemy.Factory _enemyFactory;
    [Inject] readonly Settings _settings;
    [Inject] readonly Spawn _spawn;
    int _enemiesSpawned;

    float _timeToNextSpawn;

    public void Initialize()
    {
        _timeToNextSpawn = _settings.initialWaitTime;
    }

    public void Tick()
    {
        if (_enemiesSpawned >= _settings.numberOfEnemies) return;

        if (_timeToNextSpawn > 0)
        {
            _timeToNextSpawn -= Time.deltaTime;
            return;
        }

        _timeToNextSpawn += _settings.timeBetweenSpawns;
        _enemiesSpawned += 1;

        var enemy = _enemyFactory.Create(_settings.data.prefab, _settings.data.stats);
        enemy.transform.position = _spawn.transform.position;
    }

    [Serializable]
    public class Settings
    {
        public int initialWaitTime = 0;
        public EnemyData data;
        public int numberOfEnemies;
        public float timeBetweenSpawns;
    }
}