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

    [SerializeField] private Toggle fullScreen;

    [SerializeField] private List<Button> levelButtons = new List<Button>();

    private void Start()
    {
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
    }

    public void PlayGame()
    {
        MainMenuGameObject.SetActive(false);
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
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
