using System.Collections.Generic;
using UnityEngine;

public class LinkPreview
{
    private readonly Material material;
    private readonly LayerMask obstacleMask;
    private readonly List<LineRenderer> previewLines = new();

    private GameObject ink;

    public LinkPreview(Material mat, LayerMask mask)
    {
        material = mat;
        obstacleMask = mask;
    }

    public void SetInk(GameObject newInk)
    {
        Clear();
        ink = newInk;
    }

    public void UpdatePreviewLines()
    {
        if (ink == null) return;

        if (!ink.TryGetComponent<FixeCharacter>(out var fixeChar)) return;

        List<Rigidbody2D> validOthers = new();
        foreach (var o in fixeChar.OtherInkWithLink)
            if (o != null) validOthers.Add(o);

        EnsureLineCount(validOthers.Count);

        for (int i = 0; i < validOthers.Count; i++)
        {
            var line = previewLines[i];
            if (line == null) continue;

            Vector3 a = ink.transform.position;
            Vector3 b = validOthers[i].transform.position;

            bool blocked = ObstacleUtils.IsLineBlocked(a, b, obstacleMask);

            line.enabled = true;
            line.SetPosition(0, a);
            line.SetPosition(1, b);
            line.startColor = line.endColor = blocked ? Color.red : Color.gray;
        }
    }

    public void Clear()
    {
        foreach (var line in previewLines)
            if (line != null) Object.Destroy(line.gameObject);

        previewLines.Clear();
    }

    private void EnsureLineCount(int count)
    {
        if (previewLines.Count == count) return;

        Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = new("PreviewLine");
            LineRenderer lr = obj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = lr.endWidth = 0.1f;
            lr.material = material;
            lr.startColor = lr.endColor = Color.gray;
            previewLines.Add(lr);
        }
    }
}
