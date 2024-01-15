using System;
using Cysharp.Threading.Tasks;
using ScriptableObjectData;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services
{
    public class AddressableService : IAddressableService
    {
        private readonly AssetReferencesData _assetReferencesData;

        public AddressableService(AssetReferencesData assetReferencesData)
        {
            _assetReferencesData = assetReferencesData;
        }
        
        public async UniTask<GameObject> LoadPrefab(AssetReferenceType type)
        {
            var assetReferenceGameObject = GetAssetReferenceByType(type);
            if (assetReferenceGameObject.IsValid())
            {
                return assetReferenceGameObject.Asset as GameObject;
            }

            var asyncOperation = assetReferenceGameObject.LoadAssetAsync();
            await asyncOperation.Task;
            return asyncOperation.Result;
        }

        private AssetReferenceGameObject GetAssetReferenceByType(AssetReferenceType type)
        {
            return type switch
            {
                AssetReferenceType.SquareGameObject => GetPopUpWindowPrefab(),
                
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private AssetReferenceGameObject GetPopUpWindowPrefab() => _assetReferencesData.GetSquarePrefabPrefab();
    }

    public enum AssetReferenceType
    {
        SquareGameObject
    }
}