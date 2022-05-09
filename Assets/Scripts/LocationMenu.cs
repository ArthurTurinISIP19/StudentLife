using UnityEngine;

public class LocationMenu : MonoBehaviour
{
    public static int currentLocation;
    [SerializeField] private Canvas LevelMenuCanvas;

    public void LocationSchool()
    {
        currentLocation = 1;
        ActivateLevelCanvas();
    }

    public void LocationHome()
    {
        currentLocation = 2;
        ActivateLevelCanvas();
    }
    public void LocationStreet()
    {
        currentLocation = 3;
        ActivateLevelCanvas();
    }

    private void ActivateLevelCanvas()
    {
        LevelMenuCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
