using System.Collections;
using System.Collections.Generic;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void LevelRun()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("1stRunLevel");
    }
    public void LevelEnter()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("2stEnter");
    }

    public void LevelBus()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("3rdBus");
    }
    public void RingRun()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("4thRingRun");
    }
    public void Train()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("9thWinter");
    }
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}