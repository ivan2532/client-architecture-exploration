using System.Collections;
using Features.Game.Domain;
using Features.MainMenu.Ports.Output;
using UnityEditor;
using Utility;

namespace Features.MainMenu.Domain
{
    public class MainMenuService
    {
        private readonly IMainMenuPresenter _presenter;

        private GameService _gameService;

        public MainMenuService(IMainMenuPresenter presenter)
        {
            _presenter = presenter;
        }

        public void ResolveGameService(GameService gameService)
        {
            _gameService = gameService;
        }

        public IEnumerator LoadMainMenuScene()
        {
            yield return _presenter.LoadMainMenuScene();
        }

        public void OnPlayButtonClicked()
        {
            CoroutineRunner.Run(LoadGameScene());
        }

        public void OnExitButtonClicked()
        {
            _presenter.ExitGame();
        }

        private IEnumerator LoadGameScene()
        {
            yield return _gameService.LoadGameScene();
        }
    }
}