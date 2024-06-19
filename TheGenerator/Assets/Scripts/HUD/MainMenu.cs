using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
    }

    public void StartGame(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
