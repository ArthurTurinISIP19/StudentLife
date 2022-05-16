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
        _isGameActivated = false;
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
        _isGameActivated = true;
        _canvas.enabled = true;
        _camera.transform.position = ActionCameraPoint.transform.position;
    }

    private void FixedUpdate()
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
