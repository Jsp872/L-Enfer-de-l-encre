using System.Collections.Generic;
using UnityEngine;

public class InkLinkManager : MonoBehaviour
{
    [Header("Link settings")]
    [SerializeField] private Material linkMaterial;
    [SerializeField] private LayerMask obstacleMask;

    private GameObject currentInk;
    private readonly List<CharacterLink> activeLinks = new();
    private LinkPreview linkPreview;

    private void Awake()
    {
        linkPreview = new LinkPreview(linkMaterial, obstacleMask);
    }

    public void SetCurrentInk(GameObject ink)
    {
        currentInk = ink;
        linkPreview.SetInk(ink);
    }

    public void UpdatePreviews()
    {
        if (currentInk == null) return;
        if (!currentInk.TryGetComponent(out FixeCharacter fixeChar)) return;

        if (fixeChar.isDead)
        {
            ClearPreviews();
            Destroy(currentInk);
            return;
        }

        linkPreview.UpdatePreviewLines();
    }

    public void ClearPreviews() => linkPreview.Clear();

    public bool TryCreateLinksForInk(GameObject ink)
    {
        if (!ink.TryGetComponent(out FixeCharacter fixeChar)) return false;

        int created = 0;

        foreach (var other in fixeChar.OtherInkWithLink)
        {
            if (other == null) continue;
            if (ObstacleUtils.IsLineBlocked(ink.transform.position, other.transform.position, obstacleMask))
                continue;

            var joint = ink.AddComponent<SpringJoint2D>();
            joint.connectedBody = other;
            joint.autoConfigureDistance = false;
            joint.frequency = 25f;
            joint.dampingRatio = 1f;

            CreateLink(ink.transform, other.transform);
            created++;
        }

        return created > 0;
    }

    private void CreateLink(Transform a, Transform b)
    {
        var lineObj = new GameObject("GooLink");
        var lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.startWidth = lr.endWidth = 0.2f;
        lr.material = linkMaterial;
        lr.startColor = lr.endColor = Color.black;

        var link = lineObj.AddComponent<CharacterLink>();
        link.pointA = a;
        link.pointB = b;

        activeLinks.Add(link);
        lineObj.transform.parent = a;
    }
}
