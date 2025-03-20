using System;

namespace Score
{
    public class PlayerState
    {
        private int _currentScore;

        public event Action<int> OnScoreChanged;

        public int CurrentScore => _currentScore;

        public void AddScore(int amount)
        {
            _currentScore += amount;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public void ResetScore()
        {
            _currentScore = 0;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }
}