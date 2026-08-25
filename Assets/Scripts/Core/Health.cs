using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => maxHealth;

    public bool IsAlive => CurrentHealth > 0f;

    public event Action<float, float> HealthChanged;
    public event Action Died;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount, GameObject source = null)
    {
        if (!IsAlive || amount <= 0f)
        {
            return;
        }

        CurrentHealth = Mathf.Max(
            CurrentHealth - amount,
            0f
        );

        HealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (!IsAlive || amount <= 0f)
        {
            return;
        }

        CurrentHealth = Mathf.Min(
            CurrentHealth + amount,
            maxHealth
        );

        HealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
