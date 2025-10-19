using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput), typeof(InkLinkManager))]
public class SelectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Win win;

    [Header("Settings")]
    [SerializeField] private int throwForce = 10;
    [SerializeField] private LayerMask inkLayerMask;

    private PlayerInput playerInput;
    private InkLinkManager linkManager;

    private GameObject selectedInk;
    private Rigidbody2D selectedRb;
    private bool isInkSelected;
    private Vector2 mouseWorldPos;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        linkManager = GetComponent<InkLinkManager>();
    }

    public void OnSelect()
    {
        if (win != null && win.win) return;

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0.1f, inkLayerMask);

        if (hit.collider == null) return;

        if (hit.collider.TryGetComponent(out FixeCharacter target) && !target.isSlepping)
        {
            selectedInk = target.gameObject;
            selectedRb = selectedInk.GetComponent<Rigidbody2D>();
            isInkSelected = true;

            target.animator.SetBool("isGrab", true);
            linkManager.SetCurrentInk(selectedInk);
        }
    }

    public void OnRelease()
    {
        if (!isInkSelected || selectedInk == null) return;

        isInkSelected = false;
        linkManager.ClearPreviews();

        selectedRb.AddForce((mouseWorldPos - (Vector2)selectedInk.transform.position) * throwForce);

        var fixeChar = selectedInk.GetComponent<FixeCharacter>();
        fixeChar.animator.SetBool("isGrab", false);

        if (linkManager.TryCreateLinksForInk(selectedInk))
        {
            fixeChar.isFixed = true;
            fixeChar.circleCollider2D.enabled = true;
            selectedRb.linearVelocity = Vector2.zero;
            fixeChar.circleCollider2D.gameObject.layer = 7;
            fixeChar.gameObject.layer = 8;
            selectedInk.GetComponent<SpriteRenderer>().color = Color.grey;
        }

        selectedInk = null;
    }

    private void Update()
    {
        if (!isInkSelected || selectedInk == null) return;

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        selectedInk.transform.position = mouseWorldPos;

        linkManager.UpdatePreviews();
    }

    public void OnReset()
    {
        if (win != null && win.win) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
