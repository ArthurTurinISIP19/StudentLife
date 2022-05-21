using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private Canvas _locationCanvas;
    [SerializeField] private Text[] _textLinesOnBoard;
    [SerializeField] private Text _textLineBestScore;

    public static UnityAction<int> SelectedLocation;

    private string[] _levelSchool = { "Почему опаздываем ?!","Хочу пиццу.","Белая комната.","На улице чисто.","Опять писать..." };
    private string[] _levelHome = { "Набор выживания.", "После 9 не пускаю.", "Соседи.", "Банный день.", "Главное знание." };
    private string[] _levelStreet = { "Вам повестка!", "Выходите ?", "Сидите! Сидите!", "Посещения обязательны.", "???" };


    private void OnEnable()
    {
        switch (LocationMenu.currentLocation)
        {
            case 1:
                SetLevelsText(_levelSchool);
                StartLocation();
                break;
            case 2:
                SetLevelsText(_levelHome);
                StartLocation();
                break;
            case 3:
                SetLevelsText(_levelStreet);
                StartLocation();
                break;
            default:
                break;
        }
        SetBestScoreOnBoard();
    }

    public void StartLocation()
    {
        SelectedLocation?.Invoke(LocationMenu.currentLocation);
    }

    private void SetLevelsText(string[] levels)
    {
        for (int i = 0; i < _textLinesOnBoard.Length; i++)
        {
            _textLinesOnBoard[i].text = levels[i];
        }
    }

    public void CloseLevelDisplay()
    {
        gameObject.SetActive(false);
        _locationCanvas.gameObject.SetActive(true);
    }

    public void ButtonStartLocation()
    {
        ScenesManager._instance.ButtonStartLocation();
    }
   
    private void SetBestScoreOnBoard()
    {
        var data = SaveManager.Load<SaveLoadSceneData>(ScenesManager._currentLocation);
        _textLineBestScore.text = data.bestScore.ToString();
    }
}
