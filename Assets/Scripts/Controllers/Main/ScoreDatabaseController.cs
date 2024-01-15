using UnityEngine;

namespace Controllers.Main
{
    public class ScoreDatabaseController : IScoreDatabaseController
    {
        private const string DistanceKey = "Distance";
        private const string ScoreKey = "Score";

        public void SaveDistanceAndScoreToPlayerPrefs(float distance, float score)
        {
            PlayerPrefs.SetFloat(DistanceKey, distance);
            PlayerPrefs.SetFloat(ScoreKey, score);
            PlayerPrefs.Save();
        }
        
        public (float distance, float score) LoadDistanceAndScoreFromPlayerPrefs()
        {
            return (PlayerPrefs.GetFloat(DistanceKey, 0f), PlayerPrefs.GetFloat(ScoreKey, 0f));
        }
    }
}