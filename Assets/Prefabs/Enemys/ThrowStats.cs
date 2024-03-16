using UnityEngine;

public class ThrowStats : MonoBehaviour
{
    public int damage;
    private void Start()
    {
        Destroy(gameObject, 8);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 0.1f);
    }
}
