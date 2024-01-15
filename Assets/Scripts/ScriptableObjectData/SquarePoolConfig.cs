using UnityEngine;

namespace ScriptableObjectData
{
    [CreateAssetMenu(fileName = "SquarePoolConfig", menuName = "Data/SquarePoolConfig", order = 1)]
    public class SquarePoolConfig : ScriptableObject
    {
        [SerializeField] private int poolSize = 10;
        [SerializeField] private float spawnInterval = 0.1f;
        [SerializeField] private float squareDestroyPoints = 1.0f;

        public int PoolSize => poolSize;
        public float SpawnInterval => spawnInterval;
        public float SquareDestroyPoints => squareDestroyPoints;
    }
}