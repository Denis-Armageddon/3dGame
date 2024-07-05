using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 100; // ћаксимальное количество здоровь€
    private int currentHealth; // “екущее количество здоровь€

    public HealthBar healthBar; // —сылка на компонент HealthBar

    private void Awake()
    {
        currentHealth = maxHealth;
        Debug.Log("NPC здоровье установлено на максимум: " + maxHealth);

        healthBar.SetMaxHealth(maxHealth); // ”станавливаем максимальное здоровье на полоске
    }

    // ћетод дл€ нанесени€ урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("NPC получил урон: " + damage + ". “екущее здоровье: " + currentHealth);

        healthBar.SetHealth(currentHealth); // ќбновл€ем текущее здоровье на полоске

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ћетод дл€ восстановлени€ здоровь€
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("NPC восстановил здоровье на: " + amount + ". “екущее здоровье: " + currentHealth);

        healthBar.SetHealth(currentHealth); // ќбновл€ем текущее здоровье на полоске
    }

    // ћетод дл€ проверки текущего здоровь€
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // ћетод дл€ смерти NPC
    private void Die()
    {
        Debug.Log("NPC умер.");
        // «десь можно добавить логику дл€ удалени€ NPC или проигрывани€ анимации смерти
        Destroy(gameObject);
    }
}