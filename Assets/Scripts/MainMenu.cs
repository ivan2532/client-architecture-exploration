using UnityEditor;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEngine;
#endif

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        LoadGame();
    }

    public void OnExitButtonClicked()
    {
        ExitGame();
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
