using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundDontDestroy : MonoBehaviour
{
    [SerializeField] private AudioClip _startTheme;
    [SerializeField] private AudioClip _gameTheme;
    [SerializeField] private Button _startGemeButton;
    private AudioSource _audioSource;

    private void Start()
    {
        DontDestroyOnLoad(this);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _startTheme;
        _audioSource.Play();
        _startGemeButton.onClick.AddListener(ChangeMusic);
    }

    private void ChangeMusic()
    {
        _audioSource.clip = _gameTheme;
        _audioSource.Play();
    }
}
