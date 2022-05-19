using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathExtensions : MonoBehaviour
{
    public static bool IsPointInTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 point)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = Sign(point, p1, p2);
        d2 = Sign(point, p2, p3);
        d3 = Sign(point, p3, p1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }

    private static float Sign(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    public static bool IsPointInCircle(float radius, Vector2 point, Vector2 circleCenterPoint)
    {
        return Mathf.Pow((point.x - circleCenterPoint.x), 2) + Mathf.Pow((point.y - circleCenterPoint.y), 2) < Mathf.Pow(radius, 2);
    }

    public static bool IsPointInsideSpriteRenderer(SpriteRenderer sr, Vector3 point)
    {
        return sr.bounds.Contains(point);
    }
}
