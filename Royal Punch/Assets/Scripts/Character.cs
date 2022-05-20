using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
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
}
