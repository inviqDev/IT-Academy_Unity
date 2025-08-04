using Audio;
using Particle_Systems;
using UnityEngine;

namespace Projectile
{
    public class RPG7Rocket : ProjectileBase
    {
        [SerializeField] private ParticleEffectsBase rocketTrailEffect;
        private ParticleEffectsBase _rocketTrailEffect;
        
        [SerializeField] private AudioSourceBase rocketCollisionAudioSource;
        [Range(0.01f, 1.0f)] [SerializeField] private float fireAudioSourceVolume;
        
        [SerializeField] private float explosionRadius = 5.0f;
        [SerializeField] private float explosionForce = 10.0f;
        [SerializeField] private float upwardsModifier = 2.0f;
        [SerializeField] private LayerMask affectedLayers;

        public void PlayRocketTrailEffect()
        {
            _rocketTrailEffect = Pool.Instance.GetObjectFromPool(rocketTrailEffect);

            _rocketTrailEffect.transform.SetParent(transform, false);
            _rocketTrailEffect.transform.rotation = transform.rotation;
            _rocketTrailEffect.Play(transform.localPosition, transform.rotation);
        }

        private void ExplodeRocket(Vector3 position)
        {
            var colliders = Physics.OverlapSphere(position, explosionRadius, affectedLayers);
            
            foreach (var hit in colliders)
            {
                if (!hit.TryGetComponent<Rigidbody>(out var rb)) continue;
                
                rb.transform.SetParent(null, true);
                rb.transform.localScale = Vector3.one;
                rb.isKinematic = false;
                rb.useGravity = true;
                
                rb.AddExplosionForce(explosionForce, position, explosionRadius, upwardsModifier, ForceMode.Impulse);
            }
        }

        private void PlayCollisionSound()
        {
            var collisionSound = Pool.Instance.GetObjectFromPool(rocketCollisionAudioSource);
            collisionSound.Play(transform.position, fireAudioSourceVolume);
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
            _rocketTrailEffect.transform.SetParent(null);

            ExplodeRocket(hitPoint);
            PlayCollisionSound();
            
            ReturnObjectToPool();
        }
    }
}