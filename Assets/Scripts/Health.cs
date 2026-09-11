using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDamaged;
    public event Action OnDeath;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public void Start()
    {
        currentHealth = maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth) 
            currentHealth = maxHealth;
        else if (currentHealth <= 0) 
            OnDeath?.Invoke();
        else if (amount < 0) 
            OnDamaged?.Invoke();
    }
}
