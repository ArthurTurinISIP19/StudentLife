using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractProgress : MonoBehaviour, IProgress
{
    public event UnityAction OnProgressChange;
    public void ProgressUp()
    {
        OnProgressChange?.Invoke();
    }
}
