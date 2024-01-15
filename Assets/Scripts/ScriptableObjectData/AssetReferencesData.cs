using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ScriptableObjectData
{
    [CreateAssetMenu(fileName = "AssetReferencesData", menuName = "Data/AssetReferencesData", order = 0)]
    public class AssetReferencesData : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject squarePrefab;
        
        public AssetReferenceGameObject GetSquarePrefabPrefab() => squarePrefab;
    }
}