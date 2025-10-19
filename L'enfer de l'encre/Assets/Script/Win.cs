using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private List<int> starConditions = new List<int>();
    [SerializeField] private TextMeshProUGUI remainingInk;
    public bool win;
    [SerializeField] GameObject character;
    private int numberOfInk;

    [SerializeField] private Sprite StarsUnlock;
    [SerializeField] private Sprite StarsLock;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victorySong;

    private void Start()
    {
        numberOfInk = 0;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && win == false)
        {
            victory.SetActive(true);
            audioSource.resource = victorySong;
            audioSource.Play();
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
            remainingInk.text = $"{numberOfInk} encres n'ont pas ete utilise.";
            for (int i = 0; i < starConditions.Count; i++)
            {
                Image image = stars[i].GetComponent<Image>();
                if (numberOfInk >= starConditions[i])
                {
                    image.sprite = StarsUnlock;
                    PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().name}Star{i + 1}", 1);
                }
                else
                {
                    image.sprite = StarsLock;
                    PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().name}Star{i + 1}", 0);
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
