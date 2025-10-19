using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuGameObject;
    [SerializeField] private GameObject LevelSelectGameObject;
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private GameObject AudioPanel;
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject CreditsPanel;

    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Toggle mute;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private List<Button> levelButtons = new List<Button>();
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private int numberOfLevels;
    [SerializeField] private int numberOfStarsPerLevel;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Sprite starsLock;
    [SerializeField] private Sprite starsUnlock;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (PlayerPrefs.GetInt("Level" + (i + 1), 0) == 1)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
                ColorBlock cb = levelButtons[i].colors;
                cb.disabledColor = Color.gray;
                levelButtons[i].colors = cb;
            }
        }
        for (int i = 0; i < numberOfLevels; i++)
        {
            for (int j = 0; j < numberOfStarsPerLevel; j++)
            {
                if (PlayerPrefs.GetInt("Level" + (i + 1) + "Star" + (j + 1), 0) == 1)
                {
                    Image image = stars[i * numberOfStarsPerLevel + j].GetComponent<Image>();
                    image.sprite = starsUnlock;
                }
                else
                {
                    Image image = stars[i * numberOfStarsPerLevel + j].GetComponent<Image>();
                    image.sprite = starsLock;
                }
            }

        }
        OptionPanel.SetActive(false);
        fullScreen.isOn = PlayerPrefs.GetInt("FullScreen", Screen.fullScreen ? 1 : 0) == 1;
        mute.isOn = PlayerPrefs.GetInt("Mute", audioSource.mute ? 1 : 0) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", audioSource.volume);
        Screen.fullScreen = fullScreen.isOn;
        audioSource.mute = mute.isOn;
        audioSource.volume = volumeSlider.value;
    }

    public void PlayGame()
    {
        MainMenuGameObject.SetActive(false);
        intro.SetActive(true);
        
    }

    public void SkipIntro()
    {
        intro.SetActive(false);
        LevelSelectGameObject.SetActive(true);
    }

    public void LevelSelect(string LevelToSelect)
    {
        SceneManager.LoadScene(LevelToSelect);
    }

    public void BackToMainMenu()
    {
        MainMenuGameObject.SetActive(true);
        LevelSelectGameObject.SetActive(false);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].interactable = false;
            ColorBlock cb = levelButtons[i].colors;
            cb.disabledColor = Color.gray;
            levelButtons[i].colors = cb;
        }
        for (int i = 0; i < stars.Count; i++)
        {
            Image image = stars[i].GetComponent<Image>();
            image.sprite = starsLock;
        }
    }

    public void Option()
    {
        OptionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        OptionPanel.SetActive(false);
        VideoPanel.SetActive(false);
        AudioPanel.SetActive(false);
    }

    public void Video()
    {
        VideoPanel.SetActive(true);
        AudioPanel.SetActive(false);
    }

    public void Audio()
    {
        AudioPanel.SetActive(true);
        VideoPanel.SetActive(false);
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = fullScreen.isOn;
        PlayerPrefs.SetInt("FullScreen", fullScreen.isOn ? 1 : 0);
    }

    public void MuteVolume()
    {
        audioSource.mute = mute.isOn;
        PlayerPrefs.SetInt("Mute", mute.isOn ? 1 : 0);
    }
    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        CreditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        CreditsPanel.SetActive(false);
    }
}
