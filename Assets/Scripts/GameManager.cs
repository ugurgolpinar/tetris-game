using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject restartButton;
    public Animator scoreTextAnim;
    public Animator backgroundAnim;
    private int highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", highScore);
    }

    void Update()
    {
        UpdateHighScore();

        if (TetrisBlock.gameOver)
        {
            backgroundAnim.SetTrigger("GameOver");
            scoreTextAnim.SetTrigger("GameOver");
            restartButton.SetActive(true);
        }
    }

    void UpdateHighScore()
    {
        if (TetrisBlock.score > highScore)
        {
            highScore = TetrisBlock.score;
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }

    public void RestartGame()
    {
        TetrisBlock.score = 0;
        TetrisBlock.gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
