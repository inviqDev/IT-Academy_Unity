using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class RGD5Grenade : ProjectileBase
    {
        private Rigidbody _rigidbody;
        private CapsuleCollider _collider;
        
        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
            _collider ??= GetComponent<CapsuleCollider>();

            SetDefaultGrenadeSettingToPool();
        }

        private void OnEnable()
        {
            SetDefaultGrenadeSettingToPool();
        }

        public override void Launch(Vector3 direction)
        {
            ThrowGrenade(direction);
        }
    
        private void ThrowGrenade(Vector3 direction)
        {
            transform.SetParent(null);
            _rigidbody.useGravity = true;

            _rigidbody.linearVelocity = direction * launchSpeed;
            _rigidbody.angularVelocity = new Vector3(
                Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));

            StartCoroutine(ReturnAfterLifetime(maxLifetime));
        }

        private IEnumerator ReturnAfterLifetime(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            ReturnObjectToPool();
        }

        private void SetDefaultGrenadeSettingToPool()
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.useGravity = false;
            
            _collider.isTrigger = false;
        }
    }
}
