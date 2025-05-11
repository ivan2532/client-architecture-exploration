using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.ViewModels;
using TMPro;
using UnityEngine;

namespace Features.Game.Views
{
    public class HudView : View<HudViewModel>
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private const string ScoreTextFormat = "Score: {0}";

        protected override HudViewModel CreateInitialViewModel()
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