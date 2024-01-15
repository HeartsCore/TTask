using Behaviours.Circle;
using Controllers.Circle;
using Zenject;

namespace Installers
{
    public class CircleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IClickDetector>().To<ClickDetector>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ICircleTransformProvider>().To<CircleTransformProvider>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ICircleMoveController>().To<CircleMoveController>().AsSingle();
        }
    }
}