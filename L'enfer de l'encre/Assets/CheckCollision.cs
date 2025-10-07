using System.Collections;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private Select selectScript;

    private void Start()
    {
        selectScript = GetComponentInParent<Select>();
        StartCoroutine(AutoDestroy());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && !collision.GetComponent<FixeCharacter>().isSlepping)
        {
            selectScript.haveSelectAInk = true;
            selectScript.rbSelect = collision.attachedRigidbody;
            selectScript.inkSelect = collision.gameObject;
            collision.GetComponent<FixeCharacter>().animator.SetBool("isGrab", true);
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
