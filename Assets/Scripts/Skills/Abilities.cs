using UnityEngine;

public abstract class Abilities : ScriptableObject
{
    public int cost;

    public abstract void UseAbility(Transform transform); 
}
