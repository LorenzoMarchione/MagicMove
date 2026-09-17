using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour, IHealthUI
{
    [SerializeField] private Health health;
    [SerializeField] private TMP_Text text;
    private int currentHealth;
    private int maxHealth;
    [SerializeField] private Image healthFill;
    private void OnEnable()
    {
        health.OnHealthChanged += UpdateHealth;
    }
    public void UpdateHealth()
    {
        maxHealth = health.MaxHealth;
        currentHealth = health.CurrentHealth;
        healthFill.fillAmount = (float)currentHealth / maxHealth;
    }

}
