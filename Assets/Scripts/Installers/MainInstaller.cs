using Controllers.Main;
using Controllers.Square;
using ScriptableObjectData;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferencesData assetReferencesData;
        [SerializeField] private SquarePoolConfig poolConfig;
        [SerializeField] private GameObject mainCameraPrefab;
        
        public override void InstallBindings()
        {
            BindCameraComponents();

            BindDataLoadComponents();

            BindGameControllers();
            
            BindPools();
        }

        private void BindCameraComponents()
        {
            var mainCamera = Instantiate(mainCameraPrefab).GetComponent<Camera>();
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.Bind<ICameraController>().To<CameraController>().AsSingle();
        }

        private void BindDataLoadComponents()
        {
            Container.Bind<AssetReferencesData>().FromInstance(assetReferencesData);
            Container.Bind<IAddressableService>().To<AddressableService>().AsSingle();
        }
        
        private void BindGameControllers()
        {
            Container.Bind<IScoreRecorderController>().To<ScoreRecorderController>().AsSingle();
            Container.Bind<IPathRecorderController>().To<PathRecorderController>().AsSingle();
            Container.Bind<IScoreDatabaseController>().To<ScoreDatabaseController>().AsSingle();
            Container.Bind<InputController>().AsSingle();
        }

        private void BindPools()
        {
            Container.Bind<SquarePoolConfig>().FromInstance(poolConfig);
            Container.Bind<ISquareSpawnPool>().To<SquareSpawnPool>().AsSingle();
        }
    }
}