using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    [HideInInspector] public int currentHealth;
    Enemy enemy;
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
        if (enemy != null)
        {
            if(enemy.currentState is not AlertState)
            {
                enemy.ChangeState(new AlertState());
            }
        }
        else
        {

        }
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Hit();
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
    }
}
