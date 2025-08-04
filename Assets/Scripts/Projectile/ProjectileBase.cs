using System.Collections;
using Interfaces;
using Particle_Systems;
using UnityEngine;

namespace Projectile
{
    public abstract class ProjectileBase : MonoBehaviour, IPoolable
    {
        [Header("Unique Key for pool dictionary")]
        [SerializeField] private string uniquePoolKey;
        public string UniquePoolKey => uniquePoolKey;
        
        [Header("Projectile settings")]
        [SerializeField] private protected float launchSpeed;
        [SerializeField] private protected float maxLifetime;
        
        [Header("Particle Effects settings")]
        [SerializeField] private protected ParticleEffectsBase particleHitEffect;
        
        private Coroutine _moveRoutine;

        public virtual void Launch(Vector3 direction)
        {
            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
            }

            _moveRoutine = StartCoroutine(MoveBullet(direction));
        }
        
        private IEnumerator MoveBullet(Vector3 direction)
        {
            var time = 0.0f;
            while (time < maxLifetime)
            {
                transform.position += direction * (launchSpeed * Time.deltaTime);
                time += Time.deltaTime;
                yield return null;
            }

            ReturnObjectToPool();
        }
        
        public void ReturnObjectToPool()
        {
            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
            }
            
            Pool.Instance.ReturnObjectToPool(this);
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
            
            ReturnObjectToPool();
        }
    }
}