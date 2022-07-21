using System;
using UnityEngine;
using Zenject;

public class Goal : MonoBehaviour
{
    [Inject] Settings _settings;

    int health;
    
    public void OnTriggerEnter(Collider collider)
    {
        health -= 1;
        Destroy(collider.gameObject);
    }

    [Serializable]
    public class Settings
    {
        public int maxHealth;
    }
}
