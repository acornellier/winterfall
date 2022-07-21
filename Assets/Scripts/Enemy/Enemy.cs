using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Object = UnityEngine.Object;

public class Enemy : MonoBehaviour
{
    Goal _goal;

    NavMeshAgent _navMeshAgent;
    Stats _stats;

    void Start()
    {
        transform.LookAt(_goal.transform);
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_goal.transform.position);
    }

    [Inject]
    void Construct(Stats stats, Goal goal)
    {
        _stats = stats;
        _goal = goal;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _stats.speed;
    }

    [Serializable]
    public class Stats
    {
        public float speed = 1;
    }

    public class Factory : PlaceholderFactory<Object, Stats, Enemy>
    {
    }
}