using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Win win;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnPause()
    {
        if (!isPaused && !win.win)
        {
            PauseGame();
        }
        else
        {
            Resume();
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainMenu()
    {
     Time.timeScale = 1f;
     SceneManager.LoadScene("MainMenu");
    }
}
