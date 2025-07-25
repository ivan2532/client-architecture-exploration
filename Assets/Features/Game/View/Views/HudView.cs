using Features.Game.View.Model;
using TMPro;
using UnityEngine;

namespace Features.Game.View.Views
{
    public class HudView : GameView<HudViewModel>
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private const string ScoreTextFormat = "Score: {0}";

        protected override HudViewModel Initialize()
        {
            ViewModelUpdated += OnViewModelUpdated;
            return new HudViewModel(0);
        }

        private void OnViewModelUpdated(HudViewModel viewModel)
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            scoreText.text = string.Format(ScoreTextFormat, ViewModel.Score);
        }
    }
}