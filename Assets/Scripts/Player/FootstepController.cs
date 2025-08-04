using Audio;
using UnityEngine;

namespace Player
{
    public class FootstepController : MonoBehaviour
    {
        [SerializeField] private AudioSourceBase footstepAudioSourcePrefab;
        [Range(0.01f, 1.0f)] [SerializeField] private float stepAudioSourceVolume;

        [SerializeField] private Transform stepSoundPoint;
        [SerializeField] private float stepDistance = 3.0f;
        [SerializeField] private float minMovingSpeed = 0.71f;

        private CharacterController _controller;
        private float _currentSpeed;
        
        private Vector3 _lastStepPosition;
        private float _distancePassed;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!_controller)
            {
                Debug.Log("Character Controller component is not found");
                return;
            }
            
            var horizontalVelocity = new Vector3(_controller.velocity.x, 0, _controller.velocity.z);
            _currentSpeed = horizontalVelocity.sqrMagnitude;

            if (_controller.isGrounded && _currentSpeed > minMovingSpeed * minMovingSpeed)
            {
                _distancePassed += Vector3.Distance(transform.position, _lastStepPosition);

                if (_distancePassed >= stepDistance)
                {
                    PlayFootstepsAudioClip();
                    _distancePassed = 0f;
                }

                _lastStepPosition = transform.position;
            }
            else
            {
                _lastStepPosition = transform.position;
                _distancePassed = 0f;
            }
        }

        private void PlayFootstepsAudioClip()
        {
            var footstep = Pool.Instance.GetObjectFromPool(footstepAudioSourcePrefab);
            footstep.Play(stepSoundPoint.position, stepAudioSourceVolume);
        }
    }
}
