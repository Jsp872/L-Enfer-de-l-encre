using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class TypeOfCharacter : MonoBehaviour
{
    [SerializeField] private List<AnimatorController> typeOfCharacterForAnim = new List<AnimatorController>();
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public enum CharacterType
    {
        Normal,
        Heavy,
        Light
    }
    public CharacterType characterType;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (characterType)
        {
            case CharacterType.Normal:
                rb.mass = 1f;
                animator.runtimeAnimatorController = typeOfCharacterForAnim[0];
                break;
            case CharacterType.Heavy:
                rb.mass = 3f;
                animator.runtimeAnimatorController = typeOfCharacterForAnim[1];
                break;
            case CharacterType.Light:
                rb.gravityScale = -1f;
                animator.runtimeAnimatorController = typeOfCharacterForAnim[2];
                break;
            default:
                Debug.Log("Existe pas");
                break;
        }
    }
}