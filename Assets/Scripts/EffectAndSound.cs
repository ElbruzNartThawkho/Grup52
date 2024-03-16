using UnityEngine;
public class EffectAndSound : MonoBehaviour
{
    [SerializeField] AudioClip gunSound, meleeAttackSound, RangerAttackSound, walkSound; 
    [SerializeField] ParticleSystem gunEffect,meleeAttackEfect,rangerAttackEffect;
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
    public void MeleeEffectAndSound()
    {
        audioSource.PlayOneShot(meleeAttackSound);
        meleeAttackEfect.Play();
    }
    public void RangerEffectAndSound()
    {
        audioSource.PlayOneShot(RangerAttackSound);
        rangerAttackEffect.Play();
    }
    public void WalkSound()
    {
        audioSource.PlayOneShot(walkSound);
    }
    public void ReloadEnergy()
    {
        Player.player.energy.IncreaseEnergy(Player.player.energyRegenerationAmount);
    }
}
