using UnityEngine;

public class CharacterLink : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform pointA;
    public Transform pointB;
    private float offset;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (pointA != null && pointB != null)
        {
            lineRenderer.SetPosition(0, pointA.position);
            lineRenderer.SetPosition(1, pointB.position);
            offset += Time.deltaTime * 0.5f;
            lineRenderer.material.mainTextureOffset = new Vector2(offset, 0);
            return;
        }
        else if(pointA == null || pointB == null)
        {
            Destroy(gameObject);
        }
    }
}
