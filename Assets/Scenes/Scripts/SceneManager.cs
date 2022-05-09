using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private string[] _scenesSchool;
    [SerializeField] private string[] _scenesHome;
    [SerializeField] private string[] _scenesStreet;
    [SerializeField] private AbstractGameResult _gameManager;

    private List<string> _currentScenes = new List<string>();

    private int _sceneNumber = 0;
    private int _loseNumber = 0;

    private void OnEnable()
    {
        LevelDisplay.SelectedLocation += InicilizeLocationScenes;
    }
    private void OnDisable()
    {
        LevelDisplay.SelectedLocation -= InicilizeLocationScenes;
        _gameManager.GameLost -= CheckGameResult;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void InicilizeLocationScenes(int location)
    {
        if (_currentScenes.Count > 0)
        {
            _currentScenes.Clear();
        }

        switch (location)
        {
            case 1:
                _currentScenes.AddRange(_scenesSchool);
                break;
            case 2:
                _currentScenes.AddRange(_scenesHome);
                break;
            case 3:
                _currentScenes.AddRange(_scenesStreet);
                break;
            default:
                break;
        }
    }
    public void ButtonStartLocation()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_currentScenes[_sceneNumber]);
        StartCoroutine(WaitFindGetGameManager());
    }
    public void StartLocation()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_currentScenes[_sceneNumber]);
        StartCoroutine(WaitFindGetGameManager());
    }

    private void FindGetGameManager()
    {
        if (GameObject.Find("/GameManager") == true)
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<AbstractGameResult>();
            _gameManager.GameLost += CheckGameResult;

        }
        else
        {
            _gameManager = GameObject.Find("Red").GetComponent<AbstractGameResult>();
            _gameManager.GameLost += CheckGameResult;
        }
    }
    private void ChangeSceneNumber()
    {
        if (_sceneNumber != _currentScenes.Count - 1)
        {
            _sceneNumber++;
        }
        else
        {
            _sceneNumber = 0;
        }
        StartLocation();
    }

    private void CheckGameResult(bool isGameLost)
    {
        if(isGameLost == true)
        {
            _loseNumber++;
            if (_loseNumber > 2)
            {
                print("GAME OVER");
            }
            else
            {
                StartCoroutine(WaitToNextScene());
            }
        }
        else
        {
            StartCoroutine(WaitToNextScene());
        }
    }

    public IEnumerator WaitToNextScene()
    {
        yield return new WaitForSeconds(2f);
        ChangeSceneNumber();
    }
    public IEnumerator WaitFindGetGameManager()
    {
        yield return new WaitForSeconds(0.1f);
        FindGetGameManager();
    }
}
