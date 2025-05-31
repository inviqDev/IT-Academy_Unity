using UnityEngine;
using Random = UnityEngine.Random;

namespace Homework_1
{
    public class PingPongMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private float _minValue = -5f;
        private float _maxValue = 5f;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private bool _movingForward;
        private float _time;

        private float _minDistance = 0.1f;

        private void Start()
        {
            _startPosition = transform.position;
            _targetPosition = SetRandomTargetPosition();

            _movingForward = true;
            _time = 0f;
        }

        private void Update()
        {
            MoveWithDeltaTimeIncrement();
            // MoveWithPingPongMethod();
        }

        private void MoveWithDeltaTimeIncrement()
        {
            _time += Time.deltaTime * moveSpeed;

            if (_movingForward)
            {
                transform.position = Vector3.Lerp(_startPosition, _targetPosition, _time);
                if (_time < 1f) return;

                _time = 0f;
                _movingForward = false;
            }
            else
            {
                transform.position = Vector3.Lerp(_targetPosition, _startPosition, _time);
                if (_time < 1f) return;

                _time = 0f;
                _movingForward = true;
                _targetPosition = SetRandomTargetPosition();
            }
        }

        private void MoveWithPingPongMethod()
        {
            if (!_movingForward)
            {
                _movingForward = true;
                _targetPosition = SetRandomTargetPosition();
            }

            _time = Mathf.PingPong(Time.time * moveSpeed, 1f);
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _time);

            if (Vector3.SqrMagnitude(_startPosition - transform.position) > _minDistance) return;

            _movingForward = false;
            transform.position = _startPosition;
        }

        private Vector3 SetRandomTargetPosition()
        {
            return new Vector3(
                Random.Range(_minValue, _maxValue),
                Random.Range(_minValue, _maxValue),
                Random.Range(_minValue, _maxValue)
            );
        }
    }
}