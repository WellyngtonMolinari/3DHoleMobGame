using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onStateChanged -= GameStateChangedCallback;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                SetMenu();
                break;

            case GameState.GAME:
                SetGame();
                break;
        }
    }

    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    private void SetGame()
    {
        gamePanel.SetActive(true);
        menuPanel.SetActive(false);
    }

}
