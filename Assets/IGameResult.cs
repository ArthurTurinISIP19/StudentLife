using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IGameResult
{
    public abstract event UnityAction<bool> GameLost;
    void GameResult(bool isGameLost);
}
