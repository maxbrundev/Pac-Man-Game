using System.Collections;
using System.Collections.Generic;
using PacMan;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    private GameManager m_gameManager;

    public GameObject m_pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameManager.Instance;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                m_gameManager.SetGameState(GameManager.GameState.PAUSE);
                Display();
            }
            else
            {
                m_gameManager.SetGameState(GameManager.GameState.GAME);
                Hide();
            }
        }
    }

    private void Display()
    {
        m_pauseMenu.SetActive(true);
        SetTimeScale(0.0f);
        IsPaused = true;
    }

    private void Hide()
    {
        m_pauseMenu.SetActive(false);
        SetTimeScale(1.0f);
        IsPaused = false;
    }

    private void SetTimeScale(float p_value)
    {
        Time.timeScale = p_value;
    }

    public void Resume()
    {
        Hide();
    }

    public void Restart()
    {
        Hide();
        m_gameManager.SetGameState(GameManager.GameState.GAME);
        m_gameManager.RestartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
