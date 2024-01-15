using UnityEngine;

namespace Behaviours.Circle
{
    public interface ICircleTransformProvider
    {
        Transform CircleTransform { get; }
    }
}