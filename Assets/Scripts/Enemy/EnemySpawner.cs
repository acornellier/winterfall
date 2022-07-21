using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : IInitializable, ITickable
{
    readonly List<Enemy> _enemies = new();
    [Inject] readonly Enemy.Factory _enemyFactory;
    [Inject] readonly Settings _settings;
    [Inject] readonly Spawn _spawn;
    int _enemiesSpawned;

    float _timeToNextSpawn;

    public void Initialize()
    {
        _timeToNextSpawn = 10;
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

        var enemy = _enemyFactory.Create(_settings.summonData.prefab, _settings.summonData.stats);
        enemy.transform.position = _spawn.transform.position;
        _enemies.Add(enemy);
    }

    [Serializable]
    public class Settings
    {
        public EnemySummonData summonData;
        public int numberOfEnemies;
        public float timeBetweenSpawns;
    }
}