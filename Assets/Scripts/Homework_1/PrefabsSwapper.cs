using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Homework_1
{
    public class PrefabsSwapper : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs;
        private InputSystem_Actions _inputSystem;
        
        private GameObject _instance;
        private int _previousIndex = -1;
        
        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Jump.performed += OnJump;
        }

        private void Start()
        {
            _instance = null;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (!PrepareForSpawn()) return;

            bool isCreated;
            do
            {
                isCreated = SpawnRandomObject();
            } while (!isCreated);
        }

        private bool PrepareForSpawn()
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.LogError("Prefabs array is null or empty");
                return false;
            }

            if (_instance != null)
            {
                Destroy(_instance);
            }

            return true;
        }

        private bool SpawnRandomObject()
        {
            var randomIndex = Random.Range(0, prefabs.Length);
            if (randomIndex == _previousIndex) return false;
                
            _previousIndex = randomIndex;
            var randomPrefab = prefabs[randomIndex];
                
            _instance = Instantiate(randomPrefab, Vector3.zero, Quaternion.identity);
            return true;
        }

        private void OnEnable()
        {
            _inputSystem.Enable();
        }
        
        private void OnDisable()
        {
            _inputSystem.Disable();
        }
        
        private void OnDestroy()
        {
            _inputSystem.Player.Jump.performed -= OnJump;
        }
    }
}
