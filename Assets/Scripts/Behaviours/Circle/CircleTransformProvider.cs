using Controllers.Circle;
using UnityEngine;
using Zenject;

namespace Behaviours.Circle
{
    public class CircleTransformProvider : MonoBehaviour, ICircleTransformProvider
    {
        [Inject] private readonly ICircleMoveController _circleMoveController;
        
        [SerializeField] private Transform circleTransform;

        public Transform CircleTransform => circleTransform;
    }
}