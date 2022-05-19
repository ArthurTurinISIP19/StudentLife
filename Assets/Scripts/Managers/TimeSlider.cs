using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    [SerializeField] private AbstractGameResult _gameManager;
    [SerializeField] private Slider _slider;

    private void FixedUpdate()
    {
        _slider.value = _gameManager.LevelTime;
    }

}
