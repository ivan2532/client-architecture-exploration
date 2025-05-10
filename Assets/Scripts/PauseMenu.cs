using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Utility;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private Canvas pauseMenuCanvas;

    private GameInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new GameInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Menus.Pause.performed += OnPausePerformed;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        gameState.Pause();
        CursorUtility.ShowCursor();
        ShowPauseMenu();
    }

    public void OnResumeButtonClicked()
    {
        gameState.Resume();
        CursorUtility.HideCursor();
        HidePauseMenu();
    }

    public void OnMainMenuButtonClicked()
    {
        gameState.Resume();
        CursorUtility.ShowCursor();
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowPauseMenu()
    {
        pauseMenuCanvas.enabled = true;
    }

    private void HidePauseMenu()
    {
        pauseMenuCanvas.enabled = false;
    }
}