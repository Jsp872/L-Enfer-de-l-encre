using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject creditsPanel;

    [Header("Settings UI")]
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Toggle mute;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource audioSource;

    [Header("Level Data")]
    [SerializeField] private List<Button> levelButtons = new();
    [SerializeField] private List<GameObject> stars = new();
    [SerializeField] private int numberOfLevels;
    [SerializeField] private int numberOfStarsPerLevel;
    [SerializeField] private Sprite starLocked;
    [SerializeField] private Sprite starUnlocked;

    private void Start()
    {
        SetupLevels();
        SetupStars();
        SettingsManager.ApplySettings(fullScreen, mute, volumeSlider, audioSource);
        optionPanel.SetActive(false);
    }

    private void SetupLevels()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + (i + 1), 0) == 1;
            levelButtons[i].interactable = unlocked;
            var cb = levelButtons[i].colors;
            cb.disabledColor = Color.gray;
            levelButtons[i].colors = cb;
        }
    }

    private void SetupStars()
    {
        for (int i = 0; i < numberOfLevels; i++)
        {
            for (int j = 0; j < numberOfStarsPerLevel; j++)
            {
                bool unlocked = PlayerPrefs.GetInt($"Level{i + 1}Star{j + 1}", 0) == 1;
                Image img = stars[i * numberOfStarsPerLevel + j].GetComponent<Image>();
                img.sprite = unlocked ? starUnlocked : starLocked;
            }
        }
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        intro.SetActive(true);
    }

    public void SkipIntro()
    {
        intro.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void LevelSelect(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        SetupLevels();
        SetupStars();
    }

    public void OpenOptions()
    {
        optionPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionPanel.SetActive(false);
        videoPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void OpenVideo()
    {
        videoPanel.SetActive(true);
        audioPanel.SetActive(false);
    }

    public void OpenAudio()
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }
}
