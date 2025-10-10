using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private List<int> starConditions = new List<int>();
    [SerializeField] private TextMeshProUGUI remainingInk;
    public bool win;
    [SerializeField] GameObject character;
    private int numberOfInk;

    private void Start()
    {
        numberOfInk = 0;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && win == false)
        {
            victory.SetActive(true);
            Time.timeScale = 0f;
            win = true;
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);

            foreach (Transform child in character.transform)
            {
                if (child.gameObject.layer == 6)
                {
                    numberOfInk++;
                }
            }
            remainingInk.text = $"{numberOfInk} encres n'ont pas été utilisé.";
            for (int i = 0; i < starConditions.Count; i++)
            {
                if (numberOfInk >= starConditions[i])
                {
                    stars[i].SetActive(true);
                    PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().name}Star{i + 1}", 1);
                }
            }
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel(int nextLevelNumber)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene($"Level{nextLevelNumber}");
    }
}
