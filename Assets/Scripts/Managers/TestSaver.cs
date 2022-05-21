using UnityEngine;
using UnityEngine.UI;

public class TestSaver : MonoBehaviour
{

    [SerializeField] private int _finalScore;
    [SerializeField] private string _currentLocation;
    [SerializeField] private Text _canvasTextBestScore;
    [SerializeField] private Text _canvasTextCurrentScore;
    private SaveLoadSceneData data;


    private void Start()
    {
        _finalScore = ScenesManager._currentScore;
        ScenesManager._currentScore = 0;
        _currentLocation = ScenesManager._currentLocation;
        data = SaveManager.Load<SaveLoadSceneData>(_currentLocation);
        Save();
        ChangeCanvas();
    }

    private void ChangeCanvas()
    {
        _canvasTextBestScore.text = data.bestScore.ToString();
        _canvasTextCurrentScore.text = _finalScore.ToString();
    }

    private void Save()
    {
        if (data.bestScore <= _finalScore)
        {
            SaveManager.Save(_currentLocation, GetSave());
        }

    }

    private SaveLoadSceneData GetSave()
    {
        var data = new SaveLoadSceneData()
        {
            bestScore = _finalScore
        };
        return data;
    }
}
