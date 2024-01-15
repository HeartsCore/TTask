using System;
using System.Collections.Generic;
using System.Threading;
using Behaviours.Circle;
using Controllers.Main;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Controllers.Circle
{
    public class CircleMoveController : ICircleMoveController, IDisposable
    {
        private const float InitialSpeed = 5f;
        private const float DecelerationFactor = 0.75f;
        
        private readonly InputController _inputController;
        private readonly ICircleTransformProvider _transformProvider;
        private readonly IPathRecorderController _pathRecorderController;
        private readonly IClickDetector _clickDetector;
        private CancellationTokenSource _cancellationTokenSource;
        private TweenerCore<Vector3, Path, PathOptions> _tweener;
        private Vector3 _startPosition;
        private float _totalDistance;

        public CircleMoveController(InputController inputController, ICircleTransformProvider transformProvider,
            IPathRecorderController pathRecorderController, IClickDetector clickDetector)
        {
            _inputController = inputController;
            _transformProvider = transformProvider;
            _pathRecorderController = pathRecorderController;
            _clickDetector = clickDetector;
            _clickDetector.OnClick += StopCircleMovement;
            _pathRecorderController.OnRecordTouchPath += MoveCircleAlongPath;
        }
        
        public void Dispose()
        {
            if (_clickDetector != null) _clickDetector.OnClick -= StopCircleMovement;
            if (_pathRecorderController != null) _pathRecorderController.OnRecordTouchPath -= MoveCircleAlongPath;
            
            _inputController?.Dispose();
            _cancellationTokenSource?.Dispose();
        }
        
        private void StopCircleMovement()
        {
            Debug.Log($"Circle Stopped"); //Added this debug statement to facilitate checking the circle's motion halt. 
            ClearPath();
            DOTween.Kill(_transformProvider.CircleTransform);
        }
        
        private void MoveCircleAlongPath(List<Vector3> path)
        {
            DOTween.Kill(_transformProvider.CircleTransform);

            if (path.Count == 1)
            {
                var nextPoint = path[0];
                path.Clear();

                path.Add(_transformProvider.CircleTransform.position);
                path.Add(nextPoint);
            }
            var currentSpeed = InitialSpeed;
            _startPosition = _transformProvider.CircleTransform.position;
            
            _tweener = _transformProvider.CircleTransform
                .DOPath(path.ToArray(), CalculatePathDuration(path, currentSpeed), PathType.Linear, PathMode.TopDown2D)
                .SetEase(Ease.InOutQuad)
                .OnUpdate(() =>
                {
                    currentSpeed *= DecelerationFactor;
                }).OnComplete(ClearPath);
        }

        private float CalculatePathDuration(IReadOnlyList<Vector3> path, float speed)
        {
            _totalDistance = 0.0f;
            if (path.Count <= 1)
            {
                return speed;
            }
            
            for (var i = 1; i < path.Count; i++)
            {
                _totalDistance += Vector3.Distance(path[i - 1], path[i]);
            }

            return _totalDistance / speed;
        }
        
        private void ClearPath()
        {
            _pathRecorderController.CalculateDistanceScore(_startPosition, _transformProvider.CircleTransform.position);
            _tweener.Kill();
            _pathRecorderController.ClearPath();
        }
    }
}