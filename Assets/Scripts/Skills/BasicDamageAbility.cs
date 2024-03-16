using UnityEngine;

[CreateAssetMenu(fileName = "BasicDamage", menuName = "Abilities/BasicDamage")]
public class BasicDamageAbility : Abilities
{
    public GameObject effect;
    public int damage;

    public override void UseAbility(Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Health>().TakeDamage(damage);
            }
            GameObject gameObject = Instantiate(effect, hit.point, effect.transform.rotation);
            Destroy(gameObject, 2);
        }
    }
}
