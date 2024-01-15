using System;

namespace Behaviours.Circle
{
    public interface IClickDetector
    {
        event Action OnClick;
    }
}