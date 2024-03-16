using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                Player.player.health.TakeDamage(1);
                break;
            case "Throwable":
                Player.player.health.TakeDamage(other.GetComponent<ThrowStats>().damage);
                break;
            default:

                break;
        }
    }
}
