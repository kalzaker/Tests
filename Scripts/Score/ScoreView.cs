using TMPro;
using UnityEngine;
using Zenject;

namespace Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private PlayerState _playerState;
        
        [Inject]
        private void Construct(PlayerState playerState)
        {
            _playerState = playerState;
        }

        private void Awake()
        {
            _playerState.OnScoreChanged += UpdateScoreDisplay;
        }

        private void Start()
        {
            scoreText.text = "Score: ";
        }

        private void OnDestroy()
        {
            _playerState.OnScoreChanged -= UpdateScoreDisplay;
        }

        private void UpdateScoreDisplay(int newScore)
        {
            scoreText.text = $"Score: {newScore}";
        }
    }
}