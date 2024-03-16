using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealHealPref : MonoBehaviour
{
    [SerializeField] int time, stealHeal;
    List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        InvokeRepeating(nameof(StealHeal), 0, 0.5f);
        Destroy(gameObject, time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.GetComponent<Enemy>());
        }
    }

    void StealHeal()
    {
        int heal = 0;
        foreach (Enemy enemy in enemies)
        {
            enemy.GetComponent<Health>().TakeDamage(stealHeal);
            heal += stealHeal;
        }
        Player.player.health.Heal(heal);
    }
}
