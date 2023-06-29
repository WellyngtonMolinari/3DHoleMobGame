using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { MENU, GAME, LEVELCOMPLETE, GAMEOVER }

public class GameManager : MonoBehaviour
{
    [Header(" Settings ")]
    private GameState gameState;

    [Header(" Events")]
    public static Action<GameState> onStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.MENU;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState()
    {
        gameState = GameState.GAME;
        onStateChanged?.Invoke(gameState);
    
    }

}
