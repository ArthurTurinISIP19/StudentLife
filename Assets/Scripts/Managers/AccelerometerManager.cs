using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AccelerometerManager : AbstractGameResult
{
    [SerializeField] private Accelerometer _game;
    [SerializeField] private StartAnimationAnimator _startAnimation;
    [SerializeField] private float _time = 10f;


    public override float LevelTime { get => _time; set => _time = value; }

    public override event UnityAction<bool> GameLost;

    private void OnEnable()
    {
        if (_startAnimation != null)
        {
            _startAnimation.StarAnimationeEnd += StartTimer;
        }
        _game.IsAccelerometerLost += GameResult;
    }

    private void OnDisable()
    {
        if (_startAnimation != null)
        {
            _startAnimation.StarAnimationeEnd -= StartTimer;
        }
        _game.IsAccelerometerLost -= GameResult;
    }
    private void StartTimer()
    {
        StartCoroutine(Timer());
        _startAnimation.StarAnimationeEnd -= StartTimer;
    }

    public override void GameResult(bool isGameLost)
    {
        GameLost?.Invoke(isGameLost);
    }
    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            _time -= 0.01f;
            if (_time <= 0f)
            {
                GameResult(false);
                break;
            }
        }
    }
}
