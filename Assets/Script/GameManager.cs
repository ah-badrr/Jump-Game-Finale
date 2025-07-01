using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Lives")]
    public int maxLives = 3;
    private int currentLives;
    public TMP_Text livesText;

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText; // atau TMP_Text jika pakai TextMeshPro
    public TMP_Text victoryText; // atau TMP_Text jika pakai TextMeshPro
    public TMP_Text menu; // atau TMP_Text jika pakai TextMeshPro
    private bool gameEnded = false;


    [Header("Checkpoint")]
    public Vector3 checkpointPosition;
    private Transform player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentLives = 3;
        checkpointPosition = player.position;
        UpdateUI();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }

    public void ReachCheckpoint(Vector3 pos)
    {
        checkpointPosition = pos;
    }

    public void TakeDamage()
    {
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            RestartLevel();
        }
        else
        {
            Respawn();
        }
    }

    public void AddLife(int value)
    {
        currentLives += value;

        // Maksimum nyawa tidak boleh lebih dari batas
        // if (currentLives > maxLives)
        //     currentLives = maxLives;

        UpdateUI();
    }

    void Respawn()
    {
        player.position = checkpointPosition;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void Update()
    {
        if (gameEnded && victoryText)
        {
            // Tekan spasi untuk restart
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }

            // Tekan Esc untuk keluar
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }
    }
    public void TriggerVictory()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(true);
            victoryText.text = "Victory!";
            menu.text = "Press Space to restart game and Esc to quit game!";
        }

        // (Opsional) Pause game
        Time.timeScale = 0f;
    }
    
        public void RestartGame()
    {
        Time.timeScale = 1f; // Kembalikan waktu normal
        SceneManager.LoadScene("Lobby");
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
