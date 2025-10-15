using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextToShowWhenClickSign : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    private Image Sign;
    [SerializeField] private Sprite SignWithoutText;
    [SerializeField] private Sprite SignWithText;
    private bool isTextVisible = false;

    private void Start()
    {
        Sign = GetComponent<Image>();
        textDisplay = GetComponentInChildren<TextMeshProUGUI>(true);
        Sign.sprite = SignWithoutText;
    }
    public void OnClickSign(string textToShow)
    {
        if (isTextVisible) return;
        isTextVisible = true;
        Sign.sprite = SignWithText;
        textDisplay.gameObject.SetActive(true);
        textDisplay.text = textToShow;
        StartCoroutine(HideTextAfterDelay());
    }

    private IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(3);
        Sign.sprite = SignWithoutText;
        isTextVisible = false;
        textDisplay.gameObject.SetActive(false);
    }
}
