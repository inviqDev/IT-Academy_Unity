using Managers;
using Projectile;
using UnityEngine;

namespace Weapons.Weapon_Models
{
    public class RGD5 : WeaponBase
    {
        private RGD5Grenade _currentGrenade;
        private void Awake()
        {
            WeaponPosition = new Vector3(0.05f, 0.02f, -0.15f);
            WeaponRotation = new Quaternion(0.0f, -0.0697564706f, 0.0f, 0.997564077f);
        }

        public override void UseWeapon(Vector3 direction)
        {
            PrepareNewGrenade();
            
            if (!_currentGrenade) return;
            Debug.Log("!!!");
            
            _currentGrenade.transform.SetParent(null);
            
            _currentGrenade.Launch(direction);
            _currentGrenade = null;
            
            Invoke(nameof(PrepareNewGrenade), 0.5f);
        }
        
        private void PrepareNewGrenade()
        {
            if (_currentGrenade) return;
            
            var grenade = Pool.Instance.GetObjectFromPool(weaponProjectile) as RGD5Grenade;
            
            if (!grenade)
            {
                Debug.LogWarning("RGD5Grenade component is not found");
                throw new System.NullReferenceException();
            }
            
            grenade.transform.SetParent(gameObject.transform);
            grenade.transform.localPosition = projectileSpawnPosition.localPosition;
            grenade.transform.localRotation = projectileSpawnPosition.localRotation;
            
            _currentGrenade = grenade;
        }
        
        private protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            
            transform.localPosition = WeaponPosition;
            transform.localRotation = WeaponRotation;
            
            PrepareNewGrenade();
        }
    }
}