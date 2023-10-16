using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject pauseMenu_go;

    [Header("Variables")]
    public bool _pauseMenuActive;

    private void Start()
    {
        _pauseMenuActive = false;
        pauseMenu_go.SetActive(false);
    }

    private void Update()
    {
        PlayerInput();
    }

    public void OnResumeButtonClicked()
    {
        // Resume Game:
        if (!_pauseMenuActive)
        {
            pauseMenu_go.SetActive(true);
            _pauseMenuActive = true;

            Time.timeScale = 0;
        }
        else
        {
            pauseMenu_go.SetActive(false);
            _pauseMenuActive = false;

            Time.timeScale = 1;
        }
    }

    public void OnRestartButtonCLicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuButtonCLicked()
    {
        SceneManager.LoadScene(0);
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            OnResumeButtonClicked();
        }
    }
}
