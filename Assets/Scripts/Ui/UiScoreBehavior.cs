using System.Globalization;
using Controllers.Main;
using TMPro;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class UiScoreBehavior : MonoBehaviour
    {
        [Inject] private readonly IPathRecorderController _pathRecorderController;
        [Inject] private readonly IScoreRecorderController _scoreRecorderController;
        [Inject] private readonly IScoreDatabaseController _scoreDatabaseController;
        
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text distanceText;

        private float _distance;
        private float _score;

        private void Awake()
        {
            _pathRecorderController.OnCalculatePath += SetDistanceText;
            _scoreRecorderController.OnSquareReturnToPool += SetScoreText;
            
            InitialDistanceAndScoreTexts();
        }

        private void InitialDistanceAndScoreTexts()
        {
            var result = _scoreDatabaseController.LoadDistanceAndScoreFromPlayerPrefs();
            _distance = result.distance;
            _score = result.score;
            distanceText.text = $"Distance : {_distance.ToString(CultureInfo.InvariantCulture)}";
            scoreText.text = $"Score : {_score.ToString(CultureInfo.InvariantCulture)}";
        }

        private void OnDestroy()
        {
            _pathRecorderController.OnCalculatePath -= SetDistanceText;
            _scoreRecorderController.OnSquareReturnToPool -= SetScoreText;
        }

        private void OnApplicationQuit() => _scoreDatabaseController.SaveDistanceAndScoreToPlayerPrefs(_distance, _score);
        
        private void SetDistanceText(float distanceValue)
        {
            _distance += distanceValue;
            distanceText.text = $"Distance : {_distance.ToString(CultureInfo.InvariantCulture)}";
        }
        
        private void SetScoreText(float scoreValue)
        {
            _score += scoreValue;
            scoreText.text = $"Score : {_score.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}