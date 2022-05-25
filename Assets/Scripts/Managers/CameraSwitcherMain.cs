using System.Collections;
using UnityEngine;

public class CameraSwitcherMain : MonoBehaviour
{
    [SerializeField] private AbstractGameResult game;

    [SerializeField] private StartAnimationAnimator _startAnimation;
    [SerializeField] private StartAnimationAnimator goodEndAnimation;
    [SerializeField] private StartAnimationAnimator badEndAnimation;

    [SerializeField] private GameObject StartCameraPoint;
    [SerializeField] private GameObject ActionCameraPoint;
    [SerializeField] private GameObject GoodEndCameraPoint;
    [SerializeField] private GameObject BadEndCameraPoint;


    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Canvas _canvasInstruction;

    public static bool _isGameActivated = false;
    public float _timeToEndAnimation;
    private bool _isGameOver = false;

    public static bool GameActivated
    {
        get => _isGameActivated;
    }

    private void Awake()
    {
        _isGameActivated = false;
        _isGameOver = false;
        _camera.transform.position = StartCameraPoint.transform.position;
        _canvas.enabled = false;
    }

    private void OnEnable()
    {
        _startAnimation.StarAnimationeEnd += StartActionSwitch;
        game.GameLost += GameOverCamera;        
    }
    private void OnDisable()
    {
        _startAnimation.StarAnimationeEnd -= StartActionSwitch;
        game.GameLost -= GameOverCamera;
    }

    private void StartActionSwitch()
    {
        _canvasInstruction.enabled = false;
        _isGameActivated = true;
        _canvas.enabled = true;
        _camera.transform.position = ActionCameraPoint.transform.position;
    }

    private void Update()
    {
        if (_isGameActivated)
        {
            _camera.transform.position = ActionCameraPoint.transform.position;
        }
    }

    private void GameOverCamera(bool isLost)
    {
        _isGameActivated = false;
        _canvas.enabled = false;
        if (!_isGameOver)
        {
            if (isLost)
            {
                BadEndCameraPosition();
            }
            else
            {
                GoodEndCameraPosition();
            }
        _isGameOver = true;
        }
    }
    private void BadEndCameraPosition()
    {
        _camera.transform.position = BadEndCameraPoint.transform.position;
        //badEndAnimation.Play();
        badEndAnimation.PlayBadEnd();
    }

    private void GoodEndCameraPosition()
    {
        _camera.transform.position = GoodEndCameraPoint.transform.position;
        //goodEndAnimation.Play();
        goodEndAnimation.PlayGoodEnd();

    }
}
