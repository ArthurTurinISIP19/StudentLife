using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class StartAnimation : MonoBehaviour
{
    private PlayableDirector _playableDirector;
    public UnityAction StartCutSceneEnd;
    public UnityAction StartZoom;

    private void OnEnable()
    {
        _playableDirector.stopped += EndPlayableDirector;
    }
    private void OnDisable()
    {
        _playableDirector.stopped -= EndPlayableDirector;
    }

    void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    public void Play()
    {
        _playableDirector.Play();
        StartZoom?.Invoke();
    }



    private void EndPlayableDirector(PlayableDirector playableDirector)
    {
        if(_playableDirector == playableDirector)
        {
            StartCutSceneEnd?.Invoke();
        }

    }

}
