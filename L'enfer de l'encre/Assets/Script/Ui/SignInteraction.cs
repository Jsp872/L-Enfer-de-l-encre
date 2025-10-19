using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SignInteraction : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite signWithoutText;
    [SerializeField] private Sprite signWithText;

    [Header("Settings")]
    [SerializeField] private float displayDuration = 3f;

    private TextMeshProUGUI textDisplay;
    private Image signImage;
    private bool isTextVisible;

    private void Awake()
    {
        signImage = GetComponent<Image>();
        textDisplay = GetComponentInChildren<TextMeshProUGUI>(true);
        HideTextInstant();
    }

    public void ShowText(string textToShow)
    {
        if (isTextVisible) return;

        isTextVisible = true;
        signImage.sprite = signWithText;
        textDisplay.text = textToShow;
        textDisplay.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(HideTextAfterDelay());
    }

    private IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        HideTextInstant();
    }

    private void HideTextInstant()
    {
        isTextVisible = false;
        signImage.sprite = signWithoutText;
        textDisplay.gameObject.SetActive(false);
    }
}
