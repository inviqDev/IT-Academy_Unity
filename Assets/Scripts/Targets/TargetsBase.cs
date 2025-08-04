using UnityEngine;

namespace Targets
{
    [RequireComponent(typeof(Rigidbody))]
    public class TargetsBase : MonoBehaviour
    {
        [SerializeField] private LayerMask collidableLayers;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (((1 << other.gameObject.layer) & collidableLayers.value) == 0) return;
            
            transform.SetParent(null);
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }
    }
}
