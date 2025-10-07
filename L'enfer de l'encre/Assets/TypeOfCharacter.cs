using UnityEngine;

public class TypeOfCharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public enum CharacterType
    {
        Normal,
        Heavy,
        Light
    }
    public CharacterType characterType;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (characterType)
        {
            case CharacterType.Normal:
                rb.mass = 1f;
                break;
            case CharacterType.Heavy:
                spriteRenderer.color = Color.red;
                rb.mass = 3f;
                break;
            case CharacterType.Light:
                spriteRenderer.color = Color.green;
                rb.gravityScale = -1f;
                break;
            default:
                Debug.Log("Existe pas");
                break;
        }
    }
}