using UnityEngine;

public interface IHealthUI
{
    void SetMaxHealth(int maxHealth);
    void UpdateHealth(int currentHealth);
}
