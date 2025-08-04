using Audio;
using UnityEngine;

namespace Projectile
{
    public class AK74Bullet : ProjectileBase
    {
        [SerializeField] private AudioSourceBase bulletCollisionAudioSource;
        [Range(0.01f, 1.0f)] [SerializeField] private float bulletAudioSourceVolume;
        
        private void PlayCollisionSound()
        {
            var collisionSound = Pool.Instance.GetObjectFromPool(bulletCollisionAudioSource);
            collisionSound.Play(transform.position, bulletAudioSourceVolume);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            var contact = other.GetContact(0);
            
            var hitPoint = contact.point;
            var hitNormal = contact.normal;

            var spawnPoint = hitPoint + hitNormal * 0.01f;
            var rotation = Quaternion.LookRotation(hitNormal);
            
            var hitEffect = Pool.Instance.GetObjectFromPool(particleHitEffect);
            hitEffect.Play(spawnPoint, rotation);

            PlayCollisionSound();
            
            ReturnObjectToPool();
        }
    }
}