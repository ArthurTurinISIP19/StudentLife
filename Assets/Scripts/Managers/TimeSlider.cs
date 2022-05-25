using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    [SerializeField] private AbstractGameResult _gameManager;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    private void FixedUpdate()
    {
        _slider.value = _gameManager.LevelTime;
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.15f);
        _slider.maxValue = _gameManager.LevelTime;
    }
}
