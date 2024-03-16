using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public int maxEnergy;
    [HideInInspector] public int currentEnergy;
    public static Action<int> ChangeEnergy;
    private void Awake()
    {
        currentEnergy = maxEnergy;
        ChangeEnergy?.Invoke(currentEnergy);
    }
    public void IncreaseEnergy(int amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        ChangeEnergy?.Invoke(currentEnergy);
    }

    public void DecreaseEnergy(int amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy - amount, 0, maxEnergy);
        ChangeEnergy?.Invoke(currentEnergy);
    }

    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        ChangeEnergy?.Invoke(currentEnergy);
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public int GetMaxEnergy()
    {
        return maxEnergy;
    }
}
