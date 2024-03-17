using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                
                break;
            case "Throwable":
                Player.player.health.TakeDamage(other.GetComponent<ThrowStats>().damage);
                break;
            default:

                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Player.player.health.TakeDamage(2);
                break;
            default:

                break;
        }
    }
}
