using UnityEngine;

[CreateAssetMenu(fileName = "BasicDamage", menuName = "Abilities/BasicDamage")]
public class BasicDamageAbility : Abilities
{
    public GameObject effect;
    public float damage;

    public override void UseAbility(Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                //düþmana hasar
            }
            GameObject gameObject = Instantiate(effect, hit.point, effect.transform.rotation);
            Destroy(gameObject, 2);
        }
    }
}
