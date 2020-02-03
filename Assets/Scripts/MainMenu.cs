using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject highScoreText;
    public void StartGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentScene + 1);
    }

    public void ShowScore()
    {
        highScoreText.SetActive(true);
        highScoreText.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("highscore");
    }
}
