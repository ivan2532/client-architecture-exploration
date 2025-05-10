using Core.View;
using Features.Game.ViewModel;
using TMPro;
using UnityEngine;

namespace Features.Game.View
{
    public class HudView : ViewBase<HudViewModel>
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