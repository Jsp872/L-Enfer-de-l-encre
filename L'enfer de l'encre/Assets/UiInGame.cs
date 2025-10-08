using TMPro;
using UnityEngine;

public class UiInGame : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] TextMeshProUGUI numberOfInkRemainning;

    private int numberOfInk;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
