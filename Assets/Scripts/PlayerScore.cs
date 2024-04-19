
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI collectedCandlesText;
    public TextMeshProUGUI collectedBoxesText; // Display the number of boxes collected
    public TextMeshProUGUI gameOverText; // Reference to the game over text UI element
    //public TextMeshProUGUI startMessageText; // Reference to the start message text UI element
    public TextMeshProUGUI highScoreText; // Add this for displaying high score
    private int _highScore = 0; // Variable to hold the high score
    public TextMeshProUGUI winMessageText; // Reference to the win message text UI element
    [SerializeField] HealthBar healthBar;
    [SerializeField] BulletBar bulletBar;
    [SerializeField] EnemyBar enemyBar;
    private int _score = 0;
    private int _boxCollisionCount = 0; // Counter for box collisions
    private int _enemyCollisionCount = 0;
    // private readonly int maxBoxCollisions = 5; // Maximum allowed box collisions before game over
    private readonly int maxFlamesCollision = 5;
    // private readonly int maxEnemyCollision = 5;
    private bool isGameOver = false;
    public GameObject gameOverPanel;
    public Button restartButton;

    void Start()
    {         

        _highScore = PlayerPrefs.GetInt("HighScore", 0); // Get the high score from PlayerPrefs
        _boxCollisionCount = 0;

        // Set the initial state of UI elements

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        // Hide the game over panel initially
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
        }

        if (healthBar == null || bulletBar == null)
        {
            Debug.LogError("HealthBar or BulletBar is null");
        }


        UpdateScoreText(); // Update the UI text to display the initial score
        healthBar.SetMaxHealth(100f);
        bulletBar.SetMaxBullet(0f);
        enemyBar.SetMaxHealth(100f);
    }

    public void CollectCandle()
    {
        _score++;
        bulletBar.SetBullet(2f);
        UpdateScoreText();

        if (_score == maxFlamesCollision)
        {
            //ShowWinMessage();
            healthBar.SetHealthOnFlame(10f);
        }

        if (_score == 1)
        {
            // Activate the game over text after the first candle is collected
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(false);
            }
        }
    }

    // Call this method when the player collides with a box
    public void CollideWithBox()
    {
        if (!isGameOver)
        {
            //UpdateBoxCollisionText();
            _boxCollisionCount++;
            healthBar.SetHealthOnBox(20f);

            if (healthBar.GetHealthCount() == 0)
            {
                healthBar.SetMaxHealth(0f);
                ShowGameOver();
            }
        }
    }

    // Call this method when the player collides with an enemy
    public void CollideWithEnemy()
    {
        if (!isGameOver)
        {
            //UpdateBoxCollisionText();
            _enemyCollisionCount++;
            healthBar.SetHealthOnEnemy(10f, true);
            enemyBar.ResetHealth(100);

            if (healthBar.GetHealthCount() == 0)
            {
                healthBar.SetMaxHealth(0f);
                ShowGameOver();
            }
        }
    }

    // Update the UI text to display the current score
    private void UpdateScoreText()
    {
        if (collectedCandlesText != null)
        {
            collectedCandlesText.text = "Candles: " + _score;
        }
    }

        public void ShowGameOver()
    {
        isGameOver = true;

        int finalScore = _score * 100;
        // Check if the current score is higher than the high score
        if (finalScore > _highScore)
        {
            _highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", _highScore); // Save the new high score
            PlayerPrefs.Save();
        }

        if (gameOverText != null)
        {
            gameOverText.text = "Game Over!\nYour Score: " + finalScore;
            gameOverText.gameObject.SetActive(true);
        }

        // Show the high score on the game over panel
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + _highScore;
            highScoreText.gameObject.SetActive(true);
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

  

    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // Call this method to hide the game over panel
    public void HideGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
}
