using System.Collections.Generic;
using UnityEngine;

public class FixeCharacter : MonoBehaviour
{
    public List<Rigidbody2D> OtherInkWithLink = new List<Rigidbody2D>();
    public bool isFixed = false;
    public bool canBeFixed = false;
    public bool isSlepping = false;
    public Animator animator;

    public CircleCollider2D circleCollider2D;

    [SerializeField] private GameObject victory;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (isFixed)
        {
            circleCollider2D.enabled = true;
            gameObject.layer = 8;
            circleCollider2D.gameObject.layer = 7;
        }
        if (isSlepping)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
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
                GetComponent<SpriteRenderer>().color = Color.white;
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
            Destroy(gameObject);
        }

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
