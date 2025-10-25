using UnityEngine;
using UnityEngine.SceneManagement;

public class Tlacitko : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("hra"); // nebo číslo scény
    }
}
