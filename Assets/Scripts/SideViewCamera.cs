    using UnityEngine;
    using UnityEngine.UI;

    public class SideViewCamera : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;

        [SerializeField] private Camera sideCamera;
        private readonly Vector3[] _cameraPositions = new Vector3[4];

        [SerializeField] private VehiclesContainer vehiclesContainer;
        private Transform _vehicle;

        private void InitializeCameraPositions()
        {
            _cameraPositions[0] = new Vector3(0.0f, 15.0f, 0.0f);
            _cameraPositions[1] = new Vector3(0.0f, -15.0f, 0.0f);
            _cameraPositions[2] = new Vector3(0.0f, 0.0f, -15.0f);
            _cameraPositions[3] = new Vector3(15.0f, 0.0f, 0.0f);
        }

        private void SaveParticularVehicleView(Vector3 cameraOffset)
        {
            _vehicle = vehiclesContainer.ActiveVehicle.transform;
            if (!_vehicle || !sideCamera) return;

            var currentPosition = _vehicle.position;
            var currentRotation = _vehicle.rotation;
            
            _vehicle.position = vehiclesContainer.DefaultPosition;
            _vehicle.rotation = vehiclesContainer.DefaultRotation;
            
            SaveCurrentFrameToRenderTexture(cameraOffset);
            
            _vehicle.position = currentPosition;
            _vehicle.rotation = currentRotation;
        }

        private void SaveCurrentFrameToRenderTexture(Vector3 cameraOffset)
        {
            _vehicle ??= vehiclesContainer.ActiveVehicle.transform;
            
            sideCamera.transform.position = _vehicle.position + cameraOffset;
            sideCamera.transform.LookAt(_vehicle.position);
            
            sideCamera.enabled = true;
            sideCamera.Render();
            sideCamera.enabled = false;
        }

        public void SetDefaultVehiclePositionToRenderTexture()
        {
            SaveCurrentFrameToRenderTexture(_cameraPositions[3]);
        }

        private void OnUpButtonClick() => SaveParticularVehicleView(_cameraPositions[0]);
        private void OnDownButtonClick() => SaveParticularVehicleView(_cameraPositions[1]);
        private void OnFaceButtonClick() => SaveParticularVehicleView(_cameraPositions[2]);
        private void OnLeftButtonClick() => SaveParticularVehicleView(_cameraPositions[3]);

        private void OnEnable()
        {
            if (!sideCamera) return;

            sideCamera.enabled = false;
            InitializeCameraPositions();

            buttons[0].onClick.AddListener(OnUpButtonClick);
            buttons[1].onClick.AddListener(OnDownButtonClick);
            buttons[2].onClick.AddListener(OnFaceButtonClick);
            buttons[3].onClick.AddListener(OnLeftButtonClick);
        }

        private void OnDisable()
        {
            buttons[0].onClick.RemoveListener(OnUpButtonClick);
            buttons[1].onClick.RemoveListener(OnDownButtonClick);
            buttons[2].onClick.RemoveListener(OnFaceButtonClick);
            buttons[3].onClick.RemoveListener(OnLeftButtonClick);
        }
    }