using UnityEngine;
using UnityEngine.UI;

public class PressAnyKeyToStart : MonoBehaviour
{
    public GameObject startPanel; 
    

    private bool gameStarted = false;

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
