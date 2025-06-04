using UnityEngine;
using Random = UnityEngine.Random;

namespace Homework_1
{
    public class TeleportMover : MonoBehaviour
    {
        [SerializeField] private float teleportInterval = 1f;
        
        private float _minValue = -10f;
        private float _maxValue = 10f;
        private float _timer;

        private void Start()
        {
            transform.position = SetRandomPosition();
        }

        private void Update()
        {
            if (_timer >= teleportInterval)
            {
                transform.position = SetRandomPosition();
                _timer = 0f;
            }

            _timer += Time.deltaTime;
        }

        private Vector3 SetRandomPosition()
        {
            return new Vector3(
                Random.Range(_minValue, _maxValue),
                Random.Range(_minValue, _maxValue),
                Random.Range(_minValue, _maxValue)
            );
        }
    }
}