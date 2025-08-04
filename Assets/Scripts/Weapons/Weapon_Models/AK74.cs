using Audio;
using Particle_Systems;
using UnityEngine;
using Projectile;

namespace Weapons.Weapon_Models
{
    public class AK74 : WeaponBase
    {
        [SerializeField] private ParticleEffectsBase muzzleFlashEffect;
        [SerializeField] private Transform muzzleFlashPoint;

        [SerializeField] private AudioSourceBase fireAudioSource;
        [Range(0.01f, 1.0f)] [SerializeField] private float fireAudioSourceVolume;

        private void Awake()
        {
            WeaponPosition = new Vector3(0.05f, 0.02f, -0.15f);
            WeaponRotation = new Quaternion(0.0f, -0.0697564706f, 0.0f, 0.997564077f);
        }

        public override void UseWeapon(Vector3 direction)
        {
            var bullet = PrepareNewBullet();
            bullet.transform.SetParent(null);
            bullet.Launch(direction);

            var muzzleEffect = Pool.Instance.GetObjectFromPool(muzzleFlashEffect);
            muzzleEffect.transform.SetParent(muzzleFlashPoint);
            muzzleEffect.Play(muzzleFlashPoint.position, transform.rotation);

            var shootingSound = Pool.Instance.GetObjectFromPool(fireAudioSource);
            shootingSound.Play(muzzleFlashPoint.position, fireAudioSourceVolume);
        }

        private AK74Bullet PrepareNewBullet()
        {
            var bullet = Pool.Instance.GetObjectFromPool(weaponProjectile) as AK74Bullet;

            if (!bullet)
            {
                Debug.LogWarning("AK74Bullet component is not found");
                throw new System.NullReferenceException();
            }

            bullet.transform.SetParent(gameObject.transform);
            bullet.transform.localPosition = projectileSpawnPosition.localPosition;
            bullet.transform.localRotation = Quaternion.identity;

            return bullet;
        }

        private protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            transform.localPosition = WeaponPosition;
            transform.localRotation = WeaponRotation;
        }
    }
}