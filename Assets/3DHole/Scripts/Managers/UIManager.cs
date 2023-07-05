using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;



    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI menuCoinsText;

    private void Awake()
    {
        DataManager.onCoinsUpdated += UpdateCoins;
    }

    void Start()
    {
        GameManager.onStateChanged += GameStateChangedCallback;
        PlayerTimer.onTimerOver += TimerOverCallback;
    }

    private void OnDestroy()
    {
        GameManager.onStateChanged -= GameStateChangedCallback;
        DataManager.onCoinsUpdated -= UpdateCoins;
        PlayerTimer.onTimerOver -= TimerOverCallback;
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

    private void TimerOverCallback()
    {
        // Call the GameOver() method
        GameOver();
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void UpdateCoins()
    {
        menuCoinsText.text = DataManager.instance.GetCoins().ToString();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseMenu()
    {
        pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        pausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

}
