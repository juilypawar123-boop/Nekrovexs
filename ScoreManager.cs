using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "POINTS: " + score.ToString();
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    public void AddPoint(int points = 1) // default is 1 if no argument is given
    {
        score += points;
        scoreText.text = "POINTS: " + score.ToString();

        if (score > highscore)
        {
            highscore = score;
            highscoreText.text = "HIGHSCORE: " + highscore.ToString();
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }
}
