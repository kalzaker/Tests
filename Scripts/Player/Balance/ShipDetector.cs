using Analytics;
using Enemy.Adapter;
using Score;
using UnityEngine;
using Zenject;

namespace Player.Balance
{
    public class ShipDetector : MonoBehaviour
    {
        private SceneMenu _sceneMenu;
        private PlayerState _playerState;
        private IAnalytics _eventAnalytics;
        
        [Inject]
        private void Construct(PlayerState playerState, IAnalytics eventAnalytics)
        {
            _playerState = playerState;
            _eventAnalytics = eventAnalytics;
        }

        private void Awake()
        {
            _sceneMenu = FindObjectOfType<SceneMenu>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.GetComponent<EnemyAdapter>()) return;
            
            SaveScore();
            ActivateSceneMenu();
            
            Destroy(gameObject);
        }

        private void SaveScore()
        {
            _eventAnalytics.LogEvent("Player died");
            PlayerPrefs.SetInt("CurrentScore", _playerState.CurrentScore);
        }

        private void ActivateSceneMenu()
        {
            if (_sceneMenu != null) 
                _sceneMenu.gameObject.SetActive(true);
        }
    }
}