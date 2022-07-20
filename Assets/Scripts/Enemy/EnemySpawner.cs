using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : ITickable
{
    [Inject] readonly Settings _settings;
    [Inject] readonly Enemy.Factory _enemyFactory;
    [Inject] readonly Spawn _spawn;

    float _timeToNextSpawn;
    int _enemiesSpawned;
    List<Enemy> _enemies = new();

    public void Tick()
    {
        if (_enemiesSpawned >= _settings.numberOfEnemies) return;
        // if (_enemies.Count >= 1) return;
        
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