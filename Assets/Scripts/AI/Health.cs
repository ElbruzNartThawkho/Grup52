using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    [HideInInspector] public int currentHealth;
    Enemy enemy;
    public static Action<int> ChangeHealth;
    void Start()
    {
        currentHealth = maxHealth;
        if (GetComponent<Enemy>() != null)
        {
            enemy = GetComponent<Enemy>();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Hit();
        }

        if (enemy != null)
        {
            if (enemy.currentState is not AlertState)
            {
                enemy.ChangeState(new AlertState());
            }
        }
        else
        {
            ChangeHealth?.Invoke(currentHealth);
            Debug.Log(gameObject.name);
        }
    }

    void Hit()
    {
        if (enemy != null)
        {
            enemy.animator.Play("Hit");
        }
        else
        {
            //oyuncu için bir þey olacaksa
        }
    }

    void Die()
    {
        if(enemy != null)
        {
            enemy.fieldOfView.enabled = false;
            enemy.ChangeState(new PatrolState());
            enemy.agent.SetDestination(enemy.transform.position);
            enemy.animator.Play("Die");
        }
        else
        {
            //gameover;
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (enemy == null)
        {
            ChangeHealth?.Invoke(currentHealth);
        }
    }
}
