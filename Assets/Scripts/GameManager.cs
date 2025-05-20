using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CanvasBehaviour canvasBehaviour;
    public GameObject gameOverScreen;
    public int coins = 0;
    public int lives = 10;
    public GameObject snowObject;
    public float snowCooldownDuration = 3f;
    public bool snowCooldown = true;
    public bool snowSelected = false;


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

    public async Task StartSnowCooldownTimerAsync()
    {
        // Aquí normalmente se pone snowCooldown = false antes de llamar a esta función
        await Task.Delay((int)(snowCooldownDuration * 1000));
        snowCooldown = true;
    }

}
