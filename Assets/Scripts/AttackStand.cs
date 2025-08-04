using Managers;
using UnityEngine;

public class AttackStand : MonoBehaviour
{
    [SerializeField] private Transform weaponPosition;
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(PlayerManager.Instance.PlayerTag)) return;
        
        var currentStandWeapon = PlayerManager.Instance.CurrentWeapon;
        if (!currentStandWeapon) return;
        
        currentStandWeapon.transform.SetParent(weaponPosition.transform);
        
        currentStandWeapon.transform.localPosition = Vector3.zero;
        currentStandWeapon.transform.localRotation = Quaternion.identity;
        currentStandWeapon.transform.localScale = Vector3.one;
        
        // Implement animation [rotation]
        
        PlayerManager.Instance.SetCurrentWeapon(null);
    }
}