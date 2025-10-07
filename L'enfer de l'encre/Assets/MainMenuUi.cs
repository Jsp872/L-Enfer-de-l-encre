using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private GameObject AudioPanel;

    [SerializeField] private Toggle fullScreen;
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
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
