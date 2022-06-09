using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] Button[] _buttons;

    private void OnEnable()
    {
        foreach (var item in _buttons)
        {
            item.GetComponent<Button>().onClick.AddListener(() => _audioSource.Play());
        }
    }

    private void OnDestroy()
    {
        foreach (var item in _buttons)
        {
            item.GetComponent<Button>().onClick.RemoveListener(() => _audioSource.Play());
        }
    }
}
