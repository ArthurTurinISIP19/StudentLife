using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private Canvas _locationCanvas;
    [SerializeField] private Text[] _texts;

    public static UnityAction<int> SelectedLocation;

    private string[] _levelSchool = { "������ ���������� ?!","���� �����","������� �����","�� �� ����� �����","����� ������..." };
    private string[] _levelHome = { "����� ���������", "����� 9 ����� �� ������", "������", "������� ����� �� ���", "������� ������" };
    private string[] _levelStreet = { "��� ��������", "�������� ?", "���. �������. ������", "��������� �����������", "???" };


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
    }

    public void StartLocation()
    {
        SelectedLocation?.Invoke(LocationMenu.currentLocation);
    }

    private void SetLevelsText(string[] levels)
    {
        for (int i = 0; i < _texts.Length; i++)
        {
            _texts[i].text = levels[i];
        }
    }

    public void CloseLevelDisplay()
    {
        gameObject.SetActive(false);
        _locationCanvas.gameObject.SetActive(true);
    }
}
