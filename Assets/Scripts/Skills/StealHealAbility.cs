using UnityEngine;

[CreateAssetMenu(fileName = "StealHeal", menuName = "Abilities/StealHeal")]
public class StealHealAbility : Abilities
{
    public GameObject effect;

    public override void UseAbility(Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject gameObject = Instantiate(effect, hit.point, effect.transform.rotation);
        }
    }
}
