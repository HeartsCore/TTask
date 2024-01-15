using Behaviours.Square;
using Controllers.Main;
using Cysharp.Threading.Tasks;
using ScriptableObjectData;
using Services;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Controllers.Square
{
    public class SquareSpawnPool : ISquareSpawnPool
    {
        private const int DefaultCapacity = 5;
        
        private readonly IAddressableService _addressableService;
        private readonly IScoreRecorderController _scoreRecorderController;
        private readonly int _maxPoolSize;
        private readonly float _squareDestroyPoints;
        
        private GameObject _squarePrefab;
        private Transform _squaresParent;
        private ObjectPool<GameObject> _squaresPool;

        public SquareSpawnPool(IAddressableService addressableService,
            IScoreRecorderController scoreRecorderController, SquarePoolConfig poolConfig)
        {
            _addressableService = addressableService;
            _scoreRecorderController = scoreRecorderController;
            _maxPoolSize = poolConfig.PoolSize;
            _squareDestroyPoints = poolConfig.SquareDestroyPoints;
        }
        
        public async UniTask PreloadPrefabAsync()
        {
            _squarePrefab = await _addressableService.LoadPrefab(AssetReferenceType.SquareGameObject);
        }
        
        public void Init()
        {
            _squaresParent = new GameObject("Squares").transform;
            _squaresParent.gameObject.tag = "SquaresParent";
            
            _squaresPool = new ObjectPool<GameObject>(
                createFunc: CreateSquare,
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: DestroyObject,
                defaultCapacity: DefaultCapacity,
                maxSize: _maxPoolSize);
        }
        
        private GameObject CreateSquare()
        {
            if (_squarePrefab == null)
            {
                Debug.LogError("Enemy prefab not loaded.");
                return null;
            }

            var squareGameObject = Object.Instantiate(_squarePrefab, _squaresParent);

            return squareGameObject;
        }
        
        private void DestroyObject(GameObject obj) => Object.Destroy(obj);
        
        public GameObject GetSquareFromPool(Vector3 position, Quaternion rotation)
        {
            var square = _squaresPool.Get();
            square.transform.position = position;
            square.transform.rotation = rotation;
            square.GetComponent<SquareCollisionBehaviour>().SetPoolManager(this);
            square.SetActive(true);
            return square;
        }

        public void ReturnSquareToPool(GameObject square)
        {
            _squaresPool.Release(square);
            _scoreRecorderController.RecordScore(_squareDestroyPoints);
        }

        public int GetActiveSquareCount() => _squaresPool.CountActive;
    }
}