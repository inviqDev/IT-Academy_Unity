using Interfaces;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceBase : MonoBehaviour, IPoolable
    {
        [SerializeField] private string uniquePoolKey;
        public string UniquePoolKey => uniquePoolKey;

        [SerializeField] private AudioClip audioClip;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }
    
        public virtual void Play(Vector3 position, float volume = 1f)
        {
            transform.position = position;
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;
            _audioSource.Play();

            StartCoroutine(ReturnAfterPlay());
        }
    
        private System.Collections.IEnumerator ReturnAfterPlay()
        {
            yield return new WaitWhile(() => _audioSource.isPlaying);
            ReturnObjectToPool();
        }

        public void ReturnObjectToPool()
        {
            gameObject.SetActive(false);
            Pool.Instance.ReturnObjectToPool(this);
        }
    }
}