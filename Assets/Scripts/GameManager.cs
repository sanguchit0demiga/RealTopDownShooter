using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public static GameManager Instance { get; private set; }


    public bool gameStarted = false;

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
}
