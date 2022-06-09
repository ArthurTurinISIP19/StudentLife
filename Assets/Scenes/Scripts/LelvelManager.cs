using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LelvelManager : MonoBehaviour
{
    private void Start()
    {
        CameraSwitcher._isGameActivated = false;
    }

    public void LocationMenu()
    {
        StartCoroutine(Waiter());
    }
    public void LevelRun()
    {
        SceneManager.LoadScene("1stRunLevel");
    }
    public void LevelEnter()
    {
        SceneManager.LoadScene("2stEnter");
    }

    public void LevelBus()
    {
        SceneManager.LoadScene("3rdBus");
    }
    public void RingRun()
    {
        SceneManager.LoadScene("4thRingRun");
    }
    public void Train()
    {
        SceneManager.LoadScene("9thWinter");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MainScene");

    }
}