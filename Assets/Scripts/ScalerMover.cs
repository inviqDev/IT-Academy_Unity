using UnityEngine;

namespace Homework_1
{
    public class ScalerMover : MonoBehaviour
    {
        [SerializeField] private float multiplier = 2f;
        [SerializeField] private float timeToScale;
        
        private Vector3 _defaultSize;
        
        private void Start()
        {
            _defaultSize = transform.localScale;
        }

        private void Update()
        {
            var time = Mathf.PingPong(Time.time * timeToScale, 1f);
            transform.localScale = Vector3.Lerp(_defaultSize, _defaultSize * multiplier, time);
        }
    }
}
