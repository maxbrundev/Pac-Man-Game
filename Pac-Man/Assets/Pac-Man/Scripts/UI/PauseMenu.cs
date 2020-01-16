using System.Collections;
using System.Collections.Generic;
using PacMan;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject m_pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
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
                Display();
            }
            else
            {
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
        //TODO
    }

    public void Quit()
    {
        Application.Quit();
    }
}
