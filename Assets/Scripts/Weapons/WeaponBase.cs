using Managers;
using Particle_Systems;
using Projectile;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] private protected ProjectileBase weaponProjectile;
        [SerializeField] private protected Transform projectileSpawnPosition;
        [SerializeField] private protected float fireRate;
        
        private protected Vector3 WeaponPosition;
        private protected Quaternion WeaponRotation;
        private protected readonly Vector3 DefaultScale = Vector3.one;
        
        public float FireRate => fireRate;

        public abstract void UseWeapon(Vector3 direction);

        private protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(PlayerManager.Instance.PlayerTag)) return;

            PlayerManager.Instance.SetCurrentWeapon(this);
            gameObject.transform.parent = PlayerManager.Instance.WeaponRoot;
            transform.localScale = DefaultScale;
        }
    }
}
