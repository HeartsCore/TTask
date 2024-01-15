using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public interface IAddressableService
    {
        UniTask<GameObject> LoadPrefab(AssetReferenceType type);
    }
}