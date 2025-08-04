using Interfaces;
using UnityEngine;

namespace Particle_Systems
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleEffectsBase : MonoBehaviour, IPoolable
    {
        [Header("Unique Key in pool dictionary")]
        [SerializeField] private string uniquePoolKey;
        public string UniquePoolKey => uniquePoolKey;
        
        private ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        public void Play(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            gameObject.SetActive(true);
            _particle.Play();
        }
        
        public void ReturnObjectToPool()
        {
            if (_particle.isPlaying)
            {
                _particle.Stop();
            }
            
            Pool.Instance.ReturnObjectToPool(this);
        }

        private void OnParticleSystemStopped()
        {
            ReturnObjectToPool();
        }
    }
}
