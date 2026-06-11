using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("Escena1");
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
