using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private GameObject audioPanel;

    [Header("Settings UI")]
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Toggle mute;
    [SerializeField] private Slider volumeSlider;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Gameplay References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Win win;

    private bool isPaused;

    private void Start()
    {
        SettingsManager.ApplySettings(fullScreen, mute, volumeSlider, audioSource);
        optionPanel.SetActive(false);
    }

    public void OnPause()
    {
        if (optionPanel.activeSelf) return;

        if (!isPaused && (win == null || !win.win))
            PauseGame();
        else
            Resume();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenSettings()
    {
        optionPanel.SetActive(true);
        videoPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        optionPanel.SetActive(false);
        videoPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void OpenVideoSettings()
    {
        videoPanel.SetActive(true);
        audioPanel.SetActive(false);
    }

    public void OpenAudioSettings()
    {
        audioPanel.SetActive(true);
        videoPanel.SetActive(false);
    }

    public void SetFullScreen(bool value)
    {
        SettingsManager.SetFullScreen(value);
    }

    public void SetMute(bool value)
    {
        SettingsManager.SetMute(audioSource, value);
    }

    public void SetVolume(float value)
    {
        SettingsManager.SetVolume(audioSource, value);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
