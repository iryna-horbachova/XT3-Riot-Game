using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject inventoryPanelUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void RestartLevel()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowInventoryPanel()
    {
        pauseMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
