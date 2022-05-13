using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractGameResult : MonoBehaviour, IGameResult
{
    public abstract event UnityAction<bool> GameLost;

    public abstract void GameResult(bool isGameLost);
}
