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
    public void Test()
    {
        SceneManager.LoadScene("test");
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
        SceneManager.LoadScene("5thTrainSeat");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}