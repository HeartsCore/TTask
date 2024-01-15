using System;

namespace Controllers.Main
{
    public interface IScoreRecorderController
    {
        event Action<float> OnSquareReturnToPool;
        void RecordScore(float points);
    }
}