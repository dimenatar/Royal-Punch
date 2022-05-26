using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Ragdoll _ragdoll;
    [SerializeField] private Rigidbody _rigidbody;

    private int _health;
    private int _maxHealth;
    private bool _isHitted;

    public delegate void HealthChanged(int currentHealth);

    public event HealthChanged OnHealthChanged;
    public event HealthChanged OnInitialised;
    public event Action OnDied;
    public event Action OnFallen;
    public event Action<int> OnHit;
    public bool IsHitted => _isHitted;

    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
    public int Health { get => _health; private set
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

    private void Awake()
    {
        OnDied += () => GetComponents<Collider>().ToList().ForEach(collider => collider.enabled = false);
        OnDied += () => _isHitted = false;
        _ragdoll.OnStandedUp += () => _isHitted = false;
    }

    public void Initialise(int health)
    {
        _health = health;
        MaxHealth = health;
        OnInitialised?.Invoke(Health);
        _isHitted = false;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        OnHit?.Invoke(damage);
    }

    public void GetSpecialHit(float force)
    {
        _isHitted = true;
        _ragdoll.Fall();
        OnFallen?.Invoke();
    }

    public void RestoreRagdoll()
    {
        _ragdoll.Restore();
    }

    public void Fall() => _ragdoll.Fall();
}
