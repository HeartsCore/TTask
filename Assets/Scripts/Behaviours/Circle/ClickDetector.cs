using System;
using UnityEngine;

namespace Behaviours.Circle
{
    public class ClickDetector : MonoBehaviour, IClickDetector
    {
        public event Action OnClick; 
        private void OnMouseDown() => OnClick?.Invoke();
    }
}