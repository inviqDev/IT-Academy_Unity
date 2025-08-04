using UnityEngine;
using Weapons;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField] private Transform weaponRoot;
        public Transform WeaponRoot => weaponRoot;
        
        [SerializeField] private Transform crosshairRoot;
        public Transform CrosshairRoot => crosshairRoot;

        public string PlayerTag => "Player";
        
        public WeaponBase CurrentWeapon { get; private set; }

        public void SetCurrentWeapon(WeaponBase weapon)
        {
            CurrentWeapon = weapon;
        }
    }
}
