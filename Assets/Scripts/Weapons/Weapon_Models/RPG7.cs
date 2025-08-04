using Audio;
using UnityEngine;
using Projectile;

namespace Weapons.Weapon_Models
{
    public class RPG7 : WeaponBase
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private AudioSourceBase fireAudioSource;
        [Range(0.01f, 1.0f)] [SerializeField] private float fireAudioSourceVolume;
        
        [SerializeField] private AudioSourceBase rocketAudioSource;
        [Range(0.01f, 1.0f)] [SerializeField] private float rocketAudioSourceVolume;
        
        private void Awake()
        {
            WeaponPosition = new Vector3(0.01f, 0.092f, -0.055f);
            WeaponRotation = new Quaternion(0.0f, -0.0436194055f, 0.0f, 0.999048293f);
        }

        public override void UseWeapon(Vector3 direction)
        {
            // double check null ???
            var rocket = PrepareNewRocket();
            rocket.transform.SetParent(null);
            
            rocket.Launch(direction);
            rocket.PlayRocketTrailEffect();
            
            var shootingSound = Pool.Instance.GetObjectFromPool(fireAudioSource);
            shootingSound.Play(firePoint.position, fireAudioSourceVolume);
            
            var rocketSound = Pool.Instance.GetObjectFromPool(rocketAudioSource);
            rocketSound.gameObject.transform.SetParent(rocket.transform);
            rocketSound.Play(rocketSound.transform.position, rocketAudioSourceVolume);
        }

        private RPG7Rocket PrepareNewRocket()
        {
            var rocket = Pool.Instance.GetObjectFromPool(weaponProjectile) as RPG7Rocket;
            
            if (!rocket)
            {
                Debug.LogWarning("RPG7Rocket component is not found");
                throw new System.NullReferenceException();
            }
            
            rocket.transform.SetParent(gameObject.transform);
            rocket.transform.localPosition = projectileSpawnPosition.localPosition;
            rocket.transform.localRotation = projectileSpawnPosition.localRotation;
            
            return rocket;
        }

        private protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            
            transform.localPosition = WeaponPosition;
            transform.localRotation = WeaponRotation;
        }
    }
}
