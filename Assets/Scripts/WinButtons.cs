using UnityEngine;
using UnityEngine.SceneManagement;

public class WinButtons : MonoBehaviour
{

    
    public void RePlay()
    {
        Debug.Log("Bot�n Play Again presionado");
        SceneManager.LoadScene("Game");
      
    }


    public void Exit()
    {
        Application.Quit();

    }
}
