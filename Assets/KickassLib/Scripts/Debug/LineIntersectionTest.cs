using Kickass;
using Kickass.Geometry;
using UnityEngine;

public class LineIntersectionTest : MonoBehaviour
{
    public Vector2 A1;
    public Vector2 A2;
    public Vector2 B1;
    public Vector2 B2;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(A1.ToVector3(), A2.ToVector3());

        Gizmos.color = Color.green;
        Gizmos.DrawLine(B1.ToVector3(), B2.ToVector3());

        if (Line.TryLineIntersection(A1, A2, B1, B2, out Vector2 intersection))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(intersection.ToVector3(), 0.25f);
        }

        Gizmos.matrix = Matrix4x4.identity;
    }
}