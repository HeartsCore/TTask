using Controllers.Main;
using Controllers.Square;
using UnityEngine;
using Zenject;

namespace Loading
{
    public class BootLoader : MonoBehaviour
    {
        [SerializeField] private SceneContext context;
    
        private async void Start()
        {
            var diContainer = context.Container;
            var inputController = diContainer.Resolve<InputController>();
            var squareSpawnPool = diContainer.Resolve<ISquareSpawnPool>();
            await squareSpawnPool.PreloadPrefabAsync();
            squareSpawnPool.Init();
        }
    }
}
