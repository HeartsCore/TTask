namespace Controllers.Main
{
    public interface IScoreDatabaseController
    {
        void SaveDistanceAndScoreToPlayerPrefs(float distance, float score);
        (float distance, float score) LoadDistanceAndScoreFromPlayerPrefs();
    }
}