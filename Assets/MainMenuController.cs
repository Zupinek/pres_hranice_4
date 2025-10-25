using UnityEngine;
using UnityEngine.SceneManagement; // důležité pro načítání scén

public class MainMenuController : MonoBehaviour
{
    // Tato funkce se spustí po kliknutí na tlačítko Start
    public void StartGame()
    {
        // Nahraď "GameScene" názvem své herní scény
        SceneManager.LoadScene("GameScene");
    }

    // Volitelně: tlačítko pro ukončení hry
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Hra byla ukončena."); // uvidíš jen v editoru
    }
}
