using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    void Start()
    {

    }

    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }


    public void QuitGame()
    {
        Application.Quit();

    }
}
