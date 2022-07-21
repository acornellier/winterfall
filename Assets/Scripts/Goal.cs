using System;
using UnityEngine;
using Zenject;

public class Goal : MonoBehaviour
{
    int _health;
    [Inject] Settings _settings;

    public void OnTriggerEnter(Collider trigger)
    {
        _health -= 1;
        Destroy(trigger.gameObject);
    }

    [Serializable]
    public class Settings
    {
        public int maxHealth;
    }
}