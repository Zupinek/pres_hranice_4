using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // pokud pracuješ s UI tlačítkem

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName = "GameScene"; // název scény, kterou chceš načíst

    private void Start()
    {
        // Pokud chceš, můžeš tlačítko připojit i přímo ze skriptu:
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadGameScene);
        }
    }

    public void LoadGameScene()
    {
        // Ověří, že scéna existuje v Build Settings
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("⚠️ Nebyl nastaven název scény k načtení!");
        }
    }
}
