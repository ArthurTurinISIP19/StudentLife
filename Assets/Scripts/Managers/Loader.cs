using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] private Text _school;
    [SerializeField] private Text _home;
    [SerializeField] private Text _street;

    [SerializeField] private GameObject[] _schoolImg;
    [SerializeField] private GameObject[] _homeImg;
    [SerializeField] private GameObject[] _streetImg;

    private string[] _allScenes = { "School", "Home", "Street" };
    private float _bestScoreStars;    
    private Color32 _blockerStarsColor = new Color32(0,0,0,100);

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        foreach (string item in _allScenes)
        {
            switch (item)
            {
                case "School":
                    var dataSchool = SaveManager.Load<SaveLoadSceneData>(item);
                    _school.text = dataSchool.bestScore.ToString();
                    Stars(_schoolImg, dataSchool.bestScore);
                    break;
                case "Home":
                    var dataHome = SaveManager.Load<SaveLoadSceneData>(item);
                    _home.text = dataHome.bestScore.ToString();
                    Stars(_homeImg, dataHome.bestScore);
                    break;
                case "Street":
                    var dataStreet = SaveManager.Load<SaveLoadSceneData>(item);
                    _street.text = dataStreet.bestScore.ToString();
                    Stars(_streetImg, dataStreet.bestScore);
                    break;
                default:
                    break;
            }

        }
    }

    private void Stars(GameObject[] locationStars, float bestScore)
    {
        _bestScoreStars = bestScore;

        for (int i = 0; i < 3; i++)
        {
            if(_bestScoreStars >= 500)
            {
                locationStars[i].SetActive(true);
                _bestScoreStars -= 500;
            }
            else
            {
                locationStars[i].gameObject.GetComponent<SpriteRenderer>().color = _blockerStarsColor;
            }
        }
    }

}
