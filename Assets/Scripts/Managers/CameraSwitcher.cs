using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private AbstractGameResult _gameManager;

    [SerializeField] private StartAnimation _goodEndAnimation;
    [SerializeField] private StartAnimation _badEndAnimation;

    [SerializeField] private StartAnimation _startAnimation;

    [SerializeField] private GameObject _startCameraPoint;
    [SerializeField] private GameObject _actionCameraPoint;
    [SerializeField] private GameObject _goodEndCameraPoint;
    [SerializeField] private GameObject _badEndCameraPoint;
    [SerializeField] private Canvas _canvasInstruction;

    public float _timeToEndAnimation;

    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _canvas;

    public static bool _isGameActivated = false;

    public static bool GameActivated
    {
        get => _isGameActivated;
    }

    private void Awake()
    {
        _camera.transform.position = _startCameraPoint.transform.position;
        _canvas.enabled = false;
    }

    private void OnEnable()
    {
        if (_startAnimation != null)
        {
            _startAnimation.StartCutSceneEnd += StartActionSwitch;
        }

        _gameManager.GameLost += GameOverCamera;        
    }
    private void OnDisable()
    {
        if (_startAnimation != null)
        {
            _startAnimation.StartCutSceneEnd -= StartActionSwitch;
        }
        
        _gameManager.GameLost -= GameOverCamera;
    }

    private void StartActionSwitch()
    {
        _canvasInstruction.enabled = false;
        _isGameActivated = true;
        _canvas.enabled = true;
        _camera.transform.position = _actionCameraPoint.transform.position;
    }

    private void FixedUpdate()
    {
        if (_isGameActivated)
        {
            _camera.transform.position = _actionCameraPoint.transform.position;
        }
    }

    private void GameOverCamera(bool isLost)
    {
        _isGameActivated = false;
        //_canvas.enabled = false;

        if (isLost)
        {
            BadEndCameraPosition();
        }
        else
        {
            GoodEndCameraPosition();
        }
    }
    private void BadEndCameraPosition()
    {
        _camera.transform.position = _badEndCameraPoint.transform.position;
        _badEndAnimation.Play();
    }

    private void GoodEndCameraPosition()
    {
        _camera.transform.position = _goodEndCameraPoint.transform.position;
        _goodEndAnimation.Play();
    }
}
