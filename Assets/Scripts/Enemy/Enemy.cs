using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] Image healthBarImage;

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

    void Update()
    {
        healthBarImage.transform.LookAt(Camera.main.transform);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        healthBarImage.fillAmount = _currentHealth / _stats.maxHealth;

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