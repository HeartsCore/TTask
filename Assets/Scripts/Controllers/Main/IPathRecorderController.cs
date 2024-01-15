using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Main
{
    public interface IPathRecorderController
    {
        event Action<List<Vector3>> OnRecordTouchPath;
        event Action<float> OnCalculatePath;
        void RecordTouchPath();
        void ClearPath();
        void CalculateDistanceScore(Vector3 startPosition, Vector3 finishPosition);
    }
}