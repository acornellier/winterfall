using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
    Stats _stats;
    Goal _goal;

    NavMeshAgent _navMeshAgent;

    [Inject]
    void Construct(Stats stats, Goal goal)
    {
        _stats = stats;
        _goal = goal;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _stats.speed;
        
    }

    void Start()
    {
        transform.LookAt(_goal.transform);
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_goal.transform.position);
    }

    [Serializable]
    public class Stats
    {
        public float speed = 1;
    }
    
    public class Factory : PlaceholderFactory<UnityEngine.Object, Stats, Enemy>
    {
    }
}
