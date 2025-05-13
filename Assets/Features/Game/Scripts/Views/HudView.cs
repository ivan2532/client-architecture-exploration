using Core.Infrastructure;
using Features.Game.Views.ViewModels;
using TMPro;
using UnityEngine;

namespace Features.Game.Views
{
    public class HudView : GameView<HudViewModel>
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private const string ScoreTextFormat = "Score: {0}";

        protected override HudViewModel Initialize()
        {
            return new HudViewModel(0);
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();
            UpdateScore();
        }

        private void UpdateScore()
        {
            scoreText.text = string.Format(ScoreTextFormat, ViewModel.Score);
        }
    }
}