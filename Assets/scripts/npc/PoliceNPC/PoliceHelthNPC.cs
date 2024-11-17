using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 100; // ������������ ���������� ��������
    private int currentHealth; // ������� ���������� ��������

    public HealthBar healthBar; // ������ �� ��������� HealthBar

    private void Awake()
    {
        currentHealth = maxHealth;
        Debug.Log("NPC �������� ����������� �� ��������: " + maxHealth);

        healthBar.SetMaxHealth(maxHealth); // ������������� ������������ �������� �� �������
    }

    // ����� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("NPC ������� ����: " + damage + ". ������� ��������: " + currentHealth);

        healthBar.SetHealth(currentHealth); // ��������� ������� �������� �� �������

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ����� ��� �������������� ��������
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("NPC ����������� �������� ��: " + amount + ". ������� ��������: " + currentHealth);

        healthBar.SetHealth(currentHealth); // ��������� ������� �������� �� �������
    }

    // ����� ��� �������� �������� ��������
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // ����� ��� ������ NPC
    private void Die()
    {
        Debug.Log("NPC ����.");
        // ����� ����� �������� ������ ��� �������� NPC ��� ������������ �������� ������
        Destroy(gameObject);
    }
}