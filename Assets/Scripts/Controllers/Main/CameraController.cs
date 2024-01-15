using UnityEngine;
using Zenject;

namespace Controllers.Main
{
    public class CameraController : ICameraController
    {
        private Camera _mainCamera;

        public Camera MainCamera => _mainCamera;

        [Inject]
        public void Initialize(Camera mainCamera) => _mainCamera = mainCamera;
    }
}