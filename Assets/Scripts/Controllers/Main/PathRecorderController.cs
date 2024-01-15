using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Main
{
    public class PathRecorderController : IPathRecorderController
    {
        public event Action<List<Vector3>> OnRecordTouchPath;
        public event Action<float> OnCalculatePath;

        private readonly Camera _mainCamera;
        
        private readonly List<Vector3> _touchPath = new();

        public PathRecorderController(ICameraController cameraController)
        {
            _mainCamera = cameraController.MainCamera;
        }
        
        public void RecordTouchPath()
        {
            var position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _touchPath.Add(new Vector3(position.x, position.y, 0.0f));
            
            OnRecordTouchPath?.Invoke(_touchPath);
        }

        public void ClearPath() => _touchPath.Clear();
        
        public void CalculateDistanceScore(Vector3 startPosition, Vector3 finishPosition)
        {
            var distance = Vector3.Distance(startPosition, finishPosition);
            OnCalculatePath?.Invoke(distance);
        }
    }
}