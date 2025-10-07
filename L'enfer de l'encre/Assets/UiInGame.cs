using TMPro;
using UnityEngine;

public class UiInGame : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] TextMeshProUGUI numberOfInkRemainning;

    private int numberOfInk;

    private void Start()
    {
        foreach (Transform child in character.transform)
        {
            if (child.gameObject.layer == 6)
            {
                numberOfInk++;
            }
        }
        numberOfInkRemainning.text = $"Encres restantes :" + numberOfInk;
    }

    public void UpdateNumberOfInk()
    {
        numberOfInk--;
        numberOfInkRemainning.text = $"Encres restantes :" + numberOfInk;
    }
}
