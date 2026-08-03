using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDamaged;
    public event Action OnDeath;

    public int maxHealth;
    public int currentHealth;

    public void Start()
    {
        currentHealth = maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if(currentHealth > maxHealth) 
            currentHealth = maxHealth;
        
        if(currentHealth > 0 && amount < 0) 
            OnDamaged.Invoke();
        else if (currentHealth <= 0) 
            OnDeath.Invoke();
    }
}
