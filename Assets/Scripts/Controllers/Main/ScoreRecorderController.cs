using System;

namespace Controllers.Main
{
    public class ScoreRecorderController : IScoreRecorderController
    {
        public event Action<float> OnSquareReturnToPool;
        
        public void RecordScore(float points) => OnSquareReturnToPool?.Invoke(points);
    }
}