using UnityEngine;

public static class ObstacleUtils
{
    public static bool IsLineBlocked(Vector2 from, Vector2 to, LayerMask mask)
    {
        return Physics2D.Linecast(from, to, mask).collider != null;
    }
}
