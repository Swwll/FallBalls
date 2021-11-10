using UnityEngine;

namespace UI
{
    public class LooseMenuView : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private CanvasGroup _looseMenu;
        [SerializeField] private LabeledNumberView _score;
        [SerializeField] private LabeledNumberView _bestScore;

        private void OnEnable()
        {
            _game.Lost += OnLost;
        }

        private void OnDisable()
        {
            _game.Lost -= OnLost;
        }

        private void OnLost(int score, int bestScore)
        {
            _score.SetNumber(score);
            _bestScore.SetNumber(bestScore);
            _looseMenu.alpha = 1;
            _looseMenu.interactable = true;
            _looseMenu.blocksRaycasts = true;
        }
    }
}