using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    Stats _stats;
    Goal _goal;
    NavMeshAgent _navMeshAgent;

    float _currentHealth;

    [Inject]
    void Construct(Stats stats, Goal goal)
    {
        _stats = stats;
        _goal = goal;
        _currentHealth = _stats.maxHealth;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _stats.speed;
    }

    void Start()
    {
        transform.LookAt(_goal.transform);
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_goal.transform.position);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    [Serializable]
    public class Stats
    {
        public float speed = 1;
        public int maxHealth = 1;
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, Stats, Enemy>
    {
    }
}