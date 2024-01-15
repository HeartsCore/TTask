using System;
using Controllers.Main;
using Controllers.Square;
using Cysharp.Threading.Tasks;
using ScriptableObjectData;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Behaviours.Spawner
{
    public class SquareSpawner : MonoBehaviour
    {
        [Inject] private readonly ISquareSpawnPool _squarePoolManager;
        [Inject] private readonly ICameraController _cameraController;
        [Inject] private readonly SquarePoolConfig _poolConfig;

        private Camera _mainCamera;
        private int _poolSize;
        private float _spawnInterval;
        private float _elapsedTime;
        private Vector3 _halfScreen;
        private bool _isInitialised;

        private async void Start()
        {
            _mainCamera = _cameraController.MainCamera;
            _poolSize = _poolConfig.PoolSize;
            _spawnInterval = _poolConfig.SpawnInterval;
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            _halfScreen = new Vector3(screenWidth / 2f, screenHeight / 2f, 0f);
            _isInitialised = true;
            
            while (_isInitialised)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval));

                var activeSquaresCount = _squarePoolManager.GetActiveSquareCount();

                if (activeSquaresCount < _poolSize)
                {
                    SpawnSquare();
                }
            }
        }

        private void SpawnSquare()
        {
            var randomPosition = new Vector3(Random.Range(-_halfScreen.x, _halfScreen.x), Random.Range(-_halfScreen.y, _halfScreen.y), 0f);
    
            var centeredPosition = randomPosition + new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            var worldPosition = _mainCamera.ScreenToWorldPoint(centeredPosition);
            var position = new Vector3(worldPosition.x, worldPosition.y, 0);

            _squarePoolManager.GetSquareFromPool(position, Quaternion.identity);
        }
        
        private void OnDestroy() => _isInitialised = false;
    }
}