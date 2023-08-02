using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum State
{
    Busy,
    WaitingForUser,
    TryFindMatches,
    GameOver,
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

}
