using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixeCharacter : MonoBehaviour
{
    public List<Rigidbody2D> OtherInkWithLink = new List<Rigidbody2D>();
    public bool isFixed = false;
    public bool canBeFixed = false;
    public bool isSlepping = false;
    public bool isDead = false;
    public Animator animator;

    public CircleCollider2D circleCollider2D;

    [SerializeField] private GameObject victory;

    private TypeOfCharacter typeOfCharacter;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        typeOfCharacter = GetComponent<TypeOfCharacter>();
        animator = GetComponent<Animator>();
        if (isFixed)
        {
            spriteRenderer.color = Color.grey;
            circleCollider2D.enabled = true;
            gameObject.layer = 8;
            circleCollider2D.gameObject.layer = 7;
        }
        if (isSlepping)
        {
            animator.SetBool("isSleeping", true);
            gameObject.layer = 11;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 7)
        {
            if (isSlepping)
            {
                isSlepping = false;
                gameObject.layer = 6;
                animator.SetBool("isSleeping", false);
                if (typeOfCharacter.characterType == TypeOfCharacter.CharacterType.Happy)
                {
                    rb.gravityScale = -1f;
                }
            }

            canBeFixed = true;
            Rigidbody2D parent = collision.GetComponentInParent<Rigidbody2D>();

            if (!OtherInkWithLink.Contains(parent))
            {
                OtherInkWithLink.Add(parent);
            }
        }

        if (collision.gameObject.layer == 9)
        {
            isDead = true;
            StartCoroutine(WaitForDead());
        }

    }

    private IEnumerator WaitForDead()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            canBeFixed = false;
            Rigidbody2D parent = collision.GetComponentInParent<Rigidbody2D>();
            if (OtherInkWithLink.Contains(parent) && !isFixed)
            {
                OtherInkWithLink.Remove(parent);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && isFixed)
        {
            Time.timeScale = 0f;
        }
    }
}
