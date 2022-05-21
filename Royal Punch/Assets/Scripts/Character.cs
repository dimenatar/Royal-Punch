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
        //print($"{-transform.forward * 300} {gameObject.name}");
        //_rigidbody.AddExplosionForce(100000, transform.position, 50);
        //Invoke(nameof(Fall), 1f);
        _ragdoll.Fall();
    }

    private void Fall() => _ragdoll.Fall();
}
