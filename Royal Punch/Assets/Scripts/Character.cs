using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Ragdoll _ragdoll;
    [SerializeField] private Rigidbody _rigidbody;

    private int _health;
    private int _maxHealth;

    public delegate void HealthChanged(int currentHealth);

    public event HealthChanged OnHealthChanged;
    public event Action OnDied;

    public int MaxHealth { get; private set; }
    public int Health { get => _health; set
        {
            _health = value;
            if (value > 0)
            {
                OnHealthChanged?.Invoke(Health);
            }
            else
            {
                OnHealthChanged?.Invoke(0);
                OnDied?.Invoke();
            }
        }
    }

    public void Initialise(int health)
    {
        _health = health;
        MaxHealth = health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void GetSpecialHit(Vector3 direction, float force)
    {
        _ragdoll.Fall();
    }

    public void Fall() => _ragdoll.Fall();
}
