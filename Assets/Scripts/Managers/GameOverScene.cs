using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}
