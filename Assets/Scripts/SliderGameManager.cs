using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderGameManager : AbstractGameResult
{
    [SerializeField] private AbstractProgress _game;
    [SerializeField] private StartAnimation _startAnimation;
    [SerializeField] private Slider _slider;


    private float _time = 10f;
    private float _progress = 0f;
    private float _maxProgress = 10f;
    [SerializeField] private float _progressStepUp = 0.5f;
    private float _progressStepDown = 1f;

    public override event UnityAction<bool> GameLost;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        StartCoroutine(ProgressDown());
        _slider.value = _progress;
    }

    private void OnEnable()
    {
        if (_startAnimation != null)
        {
            _startAnimation.StartCutSceneEnd += StartTimer;
        }

        _game.OnProgressChange += ProgressUp;
    }
    private void OnDisable()
    {
        if (_startAnimation != null)
        {
            _startAnimation.StartCutSceneEnd -= StartTimer;
        }

        _game.OnProgressChange -= ProgressUp;
    }
    private void StartTimer()
    {
        StartCoroutine(Timer());
        _startAnimation.StartCutSceneEnd -= StartTimer;
    }

    public override void GameResult(bool isGameLost)
    {
        StopAllCoroutines();
        GameLost?.Invoke(isGameLost);
    }

    public void SliderProgress()
    {
        _slider.value = _progress;
    }

    private void ProgressUp()
    {
        if (_progress < _maxProgress)
        {
            _progress += _progressStepUp;
            SliderProgress();
            if (_progress >= _maxProgress)
            {
                GameResult(false);
            }
        }
    }

    IEnumerator ProgressDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (_progress > 0f && _progress < _maxProgress)
            {
                _progress -= _progressStepDown;
                SliderProgress();
            }
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _time -= 0.1f;
            if (_time <= 0f)
            {
                GameResult(true);
                break;
            }
        }
    }
}

