using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private int force;
    [SerializeField] private Material line;

    [SerializeField] private LayerMask obstacleMask; // <- Layers des obstacles (murs, sol...) à définir dans l’inspector

    public bool haveSelectAInk;
    public GameObject inkSelect;
    public Rigidbody2D rbSelect;

    private Vector3 worldPos;

    private List<CharacterLink> activeLinks = new List<CharacterLink>();
    [SerializeField] private Win win;

    // --- Prévisualisation ---
    private List<LineRenderer> previewLines = new List<LineRenderer>();

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnSelect()
    {
        if (win.win) return;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        GameObject ink = Instantiate(objectToInstantiate, worldPos, Quaternion.identity, this.transform);

        inkSelect = ink;
        rbSelect = ink.GetComponent<Rigidbody2D>();
        haveSelectAInk = true;
    }

    public void OnRelease()
    {
        ClearPreviewLines();

        if (haveSelectAInk && inkSelect != null)
        {
            haveSelectAInk = false;
            rbSelect.AddForce((worldPos - inkSelect.transform.position) * force);

            FixeCharacter fixeCharacter = inkSelect.GetComponent<FixeCharacter>();
            fixeCharacter.animator.SetBool("isGrab", false);

            if (fixeCharacter.canBeFixed)
            {
                int createdJoints = 0;

                // Filtrer les autres inks valides
                List<Rigidbody2D> validOthers = new List<Rigidbody2D>();
                foreach (Rigidbody2D o in fixeCharacter.OtherInkWithLink)
                    if (o != null) validOthers.Add(o);

                foreach (Rigidbody2D other in validOthers)
                {
                    if (other == null) continue;

                    // Vérifie si un obstacle bloque la ligne
                    if (IsLineBlocked(inkSelect.transform.position, other.transform.position))
                    {
                        continue;
                    }

                    SpringJoint2D joint = inkSelect.AddComponent<SpringJoint2D>();
                    joint.connectedBody = other;
                    joint.autoConfigureDistance = false;
                    joint.frequency = 25f;
                    joint.dampingRatio = 1f;

                    CreateLink(inkSelect.transform, other.transform);
                    createdJoints++;
                }

                // On fixe l'ink seulement si au moins un joint a été créé
                if (createdJoints > 0)
                {
                    fixeCharacter.isFixed = true;
                    fixeCharacter.circleCollider2D.enabled = true;
                    rbSelect.linearVelocity = Vector2.zero;
                    fixeCharacter.circleCollider2D.gameObject.layer = 7;
                    fixeCharacter.gameObject.layer = 8;

                    inkSelect = null;
                }
            }
        }
    }

    private void Update()
    {
        if (haveSelectAInk && inkSelect != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

            inkSelect.transform.position = worldPos;

            UpdatePreviewLines();
        }
    }

    private void CreateLink(Transform a, Transform b)
    {
        GameObject lineObj = new GameObject("GooLink");
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        lr.material = line;
        lr.startColor = Color.black;
        lr.endColor = Color.black;

        CharacterLink gooLink = lineObj.AddComponent<CharacterLink>();
        gooLink.pointA = a;
        gooLink.pointB = b;

        activeLinks.Add(gooLink);
        gooLink.transform.parent = a.transform;
    }

    private void UpdatePreviewLines()
    {
        FixeCharacter fixeChar = inkSelect.GetComponent<FixeCharacter>();
        if (fixeChar == null) return;

        // Liste filtrée des autres inks valides
        List<Rigidbody2D> validOthers = new List<Rigidbody2D>();
        foreach (Rigidbody2D o in fixeChar.OtherInkWithLink)
            if (o != null) validOthers.Add(o);

        // Recréation si le nombre diffère
        if (previewLines.Count != validOthers.Count)
        {
            ClearPreviewLines();

            foreach (Rigidbody2D other in validOthers)
            {
                GameObject previewObj = new GameObject("PreviewLine");
                LineRenderer lr = previewObj.AddComponent<LineRenderer>();

                lr.positionCount = 2;
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;
                lr.material = line;
                lr.startColor = Color.gray;
                lr.endColor = Color.gray;

                previewLines.Add(lr);
            }
        }

        // Mise à jour des previews
        for (int i = 0; i < validOthers.Count; i++)
        {
            if (i >= previewLines.Count) continue;
            if (previewLines[i] == null) continue;

            Rigidbody2D other = validOthers[i];
            if (other == null) continue;

            Vector3 a = inkSelect.transform.position;
            Vector3 b = other.transform.position;

            bool blocked = IsLineBlocked(a, b);

            previewLines[i].enabled = true;
            previewLines[i].SetPosition(0, a);
            previewLines[i].SetPosition(1, b);

            if (blocked)
            {
                previewLines[i].startColor = Color.red;
                previewLines[i].endColor = Color.red;
            }
            else
            {
                previewLines[i].startColor = Color.gray;
                previewLines[i].endColor = Color.gray;
            }
        }
    }

    private void ClearPreviewLines()
    {
        foreach (var line in previewLines)
        {
            if (line != null) Destroy(line.gameObject);
        }
        previewLines.Clear();
    }

    private bool IsLineBlocked(Vector2 from, Vector2 to)
    {
        RaycastHit2D hit = Physics2D.Linecast(from, to, obstacleMask);
        return hit.collider != null;
    }

    public void OnReset()
    {
        if (win.win) return;
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
