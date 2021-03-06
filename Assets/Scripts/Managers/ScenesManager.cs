using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    //SinglTone
    public static ScenesManager _instance;

    ///???????? ????(???????) ??? ????????
    [Header("Scene's names")]
    [SerializeField] private string[] _scenesSchool;
    [SerializeField] private string[] _scenesHome;
    [SerializeField] private string[] _scenesStreet;

    [SerializeField] private float timeToLastBadAnimation = 2;

    //???????? ????(???????) ????????? ???????
    private List<string> _currentScenes = new List<string>();

    private AbstractGameResult _gameManager;
    public CameraSwitcher _cameraSwitcher;
    public CameraSwitcherMain _cameraSwitcherMain;

    //?????????? ??????????/????????? ? ????? ???? ????
    private static int _loseNumber = 0;
    private static int _gameNumber = 1;
    private static int _sceneNumber = 0;
    private static float _bonusKoef = 1f;
    public static float _timeChanger = 0f;

    //???? ? ???????(????? ???? ???)
    public static float _currentScore;
    public static string _currentLocation;

    //???????
    [SerializeField] private AdsIntestitial _adsIntestitial;

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
        ChangeTime();
        FindGetCameraSwitcher();
        
    }

    private void ChangeTime()
    {
        if(_gameNumber % 2 == 0 && _timeChanger < 2.5f)
        {
            _timeChanger += 0.5f;
        }

        _gameManager.LevelTime -= _timeChanger;


        if (GameObject.Find("Red").GetComponent<PlayerRuner>() != null && _gameNumber % 2 == 0)
        {
            PlayerRuner._multiplierStep -= 0.015f;
            EnemyRunner._addMultiplier += 0.0015f;
        }

        if (GameObject.Find("Red").GetComponent<PlayerRunnerJump>() != null && _gameNumber % 2 == 0)
        {
            PlayerRunnerJump._speed += 15f;
            PlayerRunnerJump._jump = new Vector3(0, PlayerRunnerJump._jump.y - 10, 0);
            print(PlayerRunnerJump._jump);

        }
        
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
            _bonusKoef = 1f;
            if (_loseNumber > 2)
            {
                StartCoroutine(WaitToLastBadAnimation());
                _loseNumber = 0;
                _bonusKoef = 1;
                _timeChanger = 0.2f;
                _gameNumber = 0;
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
            ChangeScore();
        }
        _gameNumber++;

    }

    private void ChangeScore()
    {
        _currentScore += 100 * _bonusKoef;
        _bonusKoef += 0.1f;
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
    private IEnumerator WaitToLastBadAnimation()
    {
        yield return new WaitForSeconds(timeToLastBadAnimation);
        _adsIntestitial.ShowAd();
        SceneManager.LoadScene("GameOver");
    }
}
