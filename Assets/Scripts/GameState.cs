using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool Paused { get; private set; }

    public void Pause()
    {
        Time.timeScale = 0;
        Paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Paused = false;
    }
}
