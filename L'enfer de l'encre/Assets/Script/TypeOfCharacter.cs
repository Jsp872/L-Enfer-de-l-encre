using System.Collections.Generic;
using UnityEngine;

public class TypeOfCharacter : MonoBehaviour
{
    [SerializeField] private List<RuntimeAnimatorController> typeOfCharacterForAnim = new List<RuntimeAnimatorController>();
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private FixeCharacter fixeCharacter;
    public enum CharacterType
    {
        Normal,
        Angry,
        Happy
    }
    public CharacterType characterType;
    void Start()
    {
        fixeCharacter = GetComponent<FixeCharacter>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (characterType)
        {
            case CharacterType.Normal:
                rb.mass = 1f;
                animator.runtimeAnimatorController = typeOfCharacterForAnim[0];
                break;
            case CharacterType.Angry:
                rb.mass = 3f;
                animator.runtimeAnimatorController = typeOfCharacterForAnim[1];
                break;
            case CharacterType.Happy:
                if (!fixeCharacter.isSlepping) rb.gravityScale = -1f;
                animator.runtimeAnimatorController = typeOfCharacterForAnim[2];
                break;
            default:
                Debug.Log("Existe pas");
                break;
        }
    }
}