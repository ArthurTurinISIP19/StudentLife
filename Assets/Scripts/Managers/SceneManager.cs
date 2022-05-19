using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager _instance;

    [SerializeField] private string[] _scenesSchool;
    [SerializeField] private string[] _scenesHome;
    [SerializeField] private string[] _scenesStreet;

    private AbstractGameResult _gameManager;
    public CameraSwitcher _cameraSwitcher;
    public CameraSwitcherMain _cameraSwitcherMain;

    private List<string> _currentScenes = new List<string>();
    private static int _loseNumber = 0;
    private int _sceneNumber = 0;

    public int _currentScore;
    private string _currentLocation;
    private string[] _allScenes = { "School", "Home", "Street" };

    [SerializeField] Text _school;
    [SerializeField] Text _home;
    [SerializeField] Text _street;

    private void OnEnable()
    {
        LevelDisplay.SelectedLocation += InicilizeLocationScenes;
    }
    private void OnDisable()
    {
        LevelDisplay.SelectedLocation -= InicilizeLocationScenes;
    }

    private void Awake()
    {
        _loseNumber = 0;
        Load();
        DontDestroyOnLoad(this);

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InicilizeLocationScenes(int location)
    {
        if (_currentScenes.Count > 0)
        {
            _sceneNumber = 0;
            _currentScenes.Clear();
        }

        switch (location)
        {
            case 1:
                _currentScenes.AddRange(_scenesSchool);
                _currentLocation = "School";
                break;
            case 2:
                _currentScenes.AddRange(_scenesHome);
                _currentLocation = "Home";
                break;
            case 3:
                _currentScenes.AddRange(_scenesStreet);
                _currentLocation = "Street";
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
        FindGetCameraSwitcher();
    }
    private void FindGetCameraSwitcher()
    {
        if (GameObject.Find("/CameraSwitcher").GetComponent<CameraSwitcher>())
        {
            _cameraSwitcher = GameObject.Find("/CameraSwitcher").GetComponent<CameraSwitcher>();
        }
        else
        {
            _cameraSwitcherMain = GameObject.Find("/CameraSwitcher").GetComponent<CameraSwitcherMain>();
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
                Save(_currentLocation);
                _currentScore = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
                _loseNumber = 0;
                _sceneNumber = 0;
            }
            else
            {
                if(_cameraSwitcher != null)
                {
                    StartCoroutine(WaitToNextScene(_cameraSwitcher._timeToEndAnimation));
                }
                else
                {
                    StartCoroutine(WaitToNextScene(_cameraSwitcherMain._timeToEndAnimation));
                }
            }
        }
        else
        {
            if (_cameraSwitcher != null)
            {
                StartCoroutine(WaitToNextScene(_cameraSwitcher._timeToEndAnimation));
            }
            else
            {
                StartCoroutine(WaitToNextScene(_cameraSwitcherMain._timeToEndAnimation));
            }
            _currentScore += 100;
        }
    }

    public IEnumerator WaitToNextScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeSceneNumber();
    }

    public IEnumerator WaitFindGetGameManager()
    {
        yield return new WaitForSeconds(0.1f);
        FindGetGameManager();
    }

    private void Load()
    {
        foreach (string item in _allScenes)
        {
            switch (item)
            {
                case "School":
                var dataSchool = SaveManager.Load<SaveLoadSceneData>(item);
                _school.text = dataSchool.bestScore.ToString();
                    break;
                case "Home":
                 var dataHome = SaveManager.Load<SaveLoadSceneData>(item);
                  _home.text = dataHome.bestScore.ToString();
                    break;
                case "Street":
                var dataStreet = SaveManager.Load<SaveLoadSceneData>(item);
                _street.text = dataStreet.bestScore.ToString();
                    break;
                default:
                    break;
            }

        } 
    }

    private void Save(string location)
    {
        var data = SaveManager.Load<SaveLoadSceneData>(location);
        if(data.bestScore <= _currentScore)
        {
            SaveManager.Save(location, GetSave());
        }
    }

    private SaveLoadSceneData GetSave()
    {
        var data = new SaveLoadSceneData()
        {
            bestScore = _currentScore
        };
        return data;
    }


}
