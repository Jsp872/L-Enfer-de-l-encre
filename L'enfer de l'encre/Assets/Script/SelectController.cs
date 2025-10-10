using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class SelectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Win win;
    [SerializeField] private InkLinkManager linkManager;

    [Header("Settings")]
    [SerializeField] private int force = 10;
    [SerializeField] private LayerMask inkLayerMask; // <- pour détecter les encres (ex: layer 6)

    private PlayerInput playerInput;
    private GameObject currentInk;
    private Rigidbody2D currentRb;
    private bool hasInkSelected;
    private Vector2 worldPos;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        linkManager = GetComponent<InkLinkManager>();
    }

    public void OnSelect()
    {
        if (win.win) return;

        // Récupération de la position souris (2D)
        worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // --- ESSAI DE SÉLECTION D'UNE ENCRE EXISTANTE ---
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0.1f, inkLayerMask);

        if (hit.collider != null)
        {
            FixeCharacter targetFixe = hit.collider.GetComponent<FixeCharacter>();
            if (targetFixe != null && !targetFixe.isSlepping)
            {
                Debug.Log($"Encre sélectionnée : {targetFixe.name}");
                currentInk = targetFixe.gameObject;
                currentRb = currentInk.GetComponent<Rigidbody2D>();
                hasInkSelected = true;

                targetFixe.animator.SetBool("isGrab", true);
                linkManager.SetCurrentInk(currentInk);
                return;
            }
            else
            {
                Debug.Log("Cette encre ne peut pas être déplacée.");
            }
        }
    }

    public void OnRelease()
    {
        if (!hasInkSelected || currentInk == null) return;

        hasInkSelected = false;
        linkManager.ClearPreviews();

        // Force de projection si c’était une nouvelle encre déplacée
        currentRb.AddForce((worldPos - (Vector2)currentInk.transform.position) * force);

        FixeCharacter fixeChar = currentInk.GetComponent<FixeCharacter>();
        fixeChar.animator.SetBool("isGrab", false);


        // Création des liens valides
        bool hasLinked = linkManager.TryCreateLinksForInk(currentInk);

        if (hasLinked)
        {
            fixeChar.isFixed = true;
            fixeChar.circleCollider2D.enabled = true;
            currentRb.linearVelocity = Vector2.zero;
            fixeChar.circleCollider2D.gameObject.layer = 7;
            fixeChar.gameObject.layer = 8;
            SpriteRenderer spriteRenderer = currentInk.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.grey;
        }

        currentInk = null;
    }

    private void Update()
    {
        if (!hasInkSelected || currentInk == null) return;

        // Mise à jour position souris (2D)
        worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Déplace uniquement sur le plan XY
        currentInk.transform.position = worldPos;

        linkManager.UpdatePreviews();
    }

    public void OnReset()
    {
        if (win.win) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
