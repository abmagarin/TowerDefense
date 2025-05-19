using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CanvasBehaviour canvasBehaviour;
    public GameObject gameOverScreen;
    public int coins = 0;
    public int lives = 10;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    void Update()
    {
        if (lives <= 0)
        {
            ShowGameOver();
        }
    }

    public int GetCoins()
    {
        return coins;
    }
    public int GetLives()
    {
        return lives;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        canvasBehaviour.UpdateCoinsLabel(coins);
    }

    public void AddLives(int amount)
    {
        lives += amount;
        canvasBehaviour.UpdateLivesLabel(lives);
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
