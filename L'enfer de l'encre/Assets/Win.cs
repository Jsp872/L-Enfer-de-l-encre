using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    public bool win;
    [SerializeField] GameObject character;
    private int numberOfInk;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
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
            Debug.Log($"Vous avez gagné ! Il vous restait {numberOfInk} encres.");
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
