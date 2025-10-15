using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Win win;
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private GameObject AudioPanel;

    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Toggle mute;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        fullScreen.isOn = PlayerPrefs.GetInt("FullScreen", Screen.fullScreen ? 1 : 0) == 1;
        mute.isOn = PlayerPrefs.GetInt("Mute", audioSource.mute ? 1 : 0) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", audioSource.volume);
        Screen.fullScreen = fullScreen.isOn;
        audioSource.mute = mute.isOn;
        audioSource.volume = volumeSlider.value;
        playerInput = GetComponent<PlayerInput>();
        OptionPanel.SetActive(false);
    }

    public void OnPause()
    {
        if (OptionPanel.activeSelf) return;
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

    public void OpenSettings()
    {
        OptionPanel.SetActive(true);
        VideoPanel.SetActive(false);
        AudioPanel.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        OptionPanel.SetActive(false);
        VideoPanel.SetActive(false);
        AudioPanel.SetActive(false);
    }

    public void OpenVideoSettings()
    {
        VideoPanel.SetActive(true);
        AudioPanel.SetActive(false);
    }

    public void OpenAudioSettings()
    {
        VideoPanel.SetActive(false);
        AudioPanel.SetActive(true);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }

    public void SetMute(bool isMute)
    {
        audioSource.mute = isMute;
        PlayerPrefs.SetInt("Mute", isMute ? 1 : 0);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void MainMenu()
    {
     Time.timeScale = 1f;
     SceneManager.LoadScene("MainMenu");
    }
}
