using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIStats : MonoBehaviour
{
    [SerializeField] Slider energy, health;
    [SerializeField] GameObject[] abilitiesImages;
    private void OnEnable()
    {
        Health.ChangeHealth += Health_ChangeHealth;
        Energy.ChangeEnergy += Energy_ChangeEnergy;
        Player.ChangeAbilities += Player_ChangeAbilities;
    }

    private void Player_ChangeAbilities(int i)
    {
        foreach (GameObject ability in abilitiesImages)
        {
            ability.SetActive(false);
        }
        abilitiesImages[i].SetActive(true);
    }

    private void Health_ChangeHealth(int i)
    {
        health.value = i;
    }

    public void Energy_ChangeEnergy(int i)
    {
        energy.value = i;
    }
    private void OnDisable()
    {
        Energy.ChangeEnergy -= Energy_ChangeEnergy;
        Health.ChangeHealth -= Health_ChangeHealth;
        Player.ChangeAbilities -= Player_ChangeAbilities;
    }
}
