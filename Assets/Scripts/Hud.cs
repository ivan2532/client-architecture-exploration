using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private const string ScoreTextFormat = "Score: {0}";
    
    private int _score;

    public void IncrementScore()
    {
        _score++;
        scoreText.text = string.Format(ScoreTextFormat, _score);
    }
}
