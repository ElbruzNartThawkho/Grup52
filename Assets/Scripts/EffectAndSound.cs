using UnityEngine;
public class EffectAndSound : MonoBehaviour
{
    [SerializeField] AudioClip gunSound; 
    [SerializeField] ParticleSystem gunEffect;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void GunEffectAndSound()
    {
        audioSource.PlayOneShot(gunSound);
        gunEffect.Play();
    }
}
