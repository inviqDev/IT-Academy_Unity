using UnityEngine;
using Random = UnityEngine.Random;

namespace Homework_1
{
    public class RotatorMover : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 2.4f;
        
        private Vector3 _direction;

        private void Start()
        {
            _direction = new Vector3(
                Random.Range(-180, 180),
                Random.Range(-180, 180),
                Random.Range(-180, 180)
            );
        }

        private void Update()
        {
            transform.Rotate(_direction * (rotationSpeed * Time.deltaTime), Space.World);
        }
    }
}