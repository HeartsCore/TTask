using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Controllers.Square
{
    public interface ISquareSpawnPool
    {
        UniTask PreloadPrefabAsync();
        void Init();
        GameObject GetSquareFromPool(Vector3 position, Quaternion rotation);
        void ReturnSquareToPool(GameObject square);
        int GetActiveSquareCount();
    }
}