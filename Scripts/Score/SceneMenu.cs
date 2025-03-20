using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Score
{
    public class SceneMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button restartButton;

        private PlayerState _playerState;
        
        [Inject]
        private void Construct(PlayerState playerState)
        {
            _playerState = playerState;
        }

        private void LateUpdate()
        {
            UpdateScoreText();
        }

        private void Start()
        {
            gameObject.SetActive(false);
            
            if (restartButton != null)
                restartButton.onClick.AddListener(RestartGame);
        }

        private void UpdateScoreText()
        {
            int currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
            scoreText.text = $"Score: {currentScore}";
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _playerState.ResetScore();
        }
    }
}