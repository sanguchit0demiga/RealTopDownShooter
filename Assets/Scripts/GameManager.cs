using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public static GameManager Instance { get; private set; }


    public bool gameStarted = false;
    public TMP_Text enemiesRemainingText;

    private int enemiesKilled = 0;
    private int maxEnemiesToKill = 10;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
        void Start()
    {
        Time.timeScale = 0f;
        UpdateEnemiesRemainingText();

    }

    void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        startPanel.SetActive(false);
        
        Time.timeScale = 1f;
        gameStarted = true;
    }
    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateEnemiesRemainingText();

        if (enemiesKilled >= maxEnemiesToKill)
        {
            WinGame();
        }
    }
    void UpdateEnemiesRemainingText()
    {
        int remaining = maxEnemiesToKill - enemiesKilled;
        enemiesRemainingText.text = "ENEMIES REMAINING: " + remaining;
        Debug.Log("Texto actualizado: " + enemiesRemainingText.text);
    }

    void WinGame()
    {
       
        
        SceneManager.LoadScene("Win"); 
    }
}
