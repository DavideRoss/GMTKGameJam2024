using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kickass
{
    public static class GizmoPlus
    {
        const float ARROW_ANGLE = 150;
        const float ARROW_LENGTH = .25f;

        public static void DrawArrow(Vector3 start, Vector3 finish)
        {
            Gizmos.DrawLine(start, finish);

            Vector3 dir = (finish - start).normalized;
            Vector3 dright = Vector3.Cross(dir, Vector3.up);

            for (int i = 0; i < 4; i++)
            {
                Vector3 end = Quaternion.AngleAxis(ARROW_ANGLE, dright) * dir;
                end = Quaternion.AngleAxis(45 + 90 * i, dir) * end;
                Gizmos.DrawLine(finish, finish + end * ARROW_LENGTH);
            }
        }

        public static void DrawArrow(Vector3 start, Vector3 dir, float distance)
        {
            DrawArrow(start, start + dir * distance);
        }

        public static void DrawVisionCone(Vector3 start, Vector3 dir, float range, float angle, int sections = 8)
        {
            Gizmos.DrawLine(start, start + dir * range);
            Vector3 p1 = start + (Quaternion.Euler(0, -angle / 2.0f, 0) * dir) * range;
            Vector3 p2 = start + (Quaternion.Euler(0, angle / 2.0f, 0) * dir) * range;

            Gizmos.DrawLine(start, p1);
            Gizmos.DrawLine(start, p2);

            Vector3 lastPoint = p1;

            for (int i = 0; i < sections; i++)
            {
                float currAngle = (-angle / 2.0f) + (angle / sections * i);
                Vector3 p = start + (Quaternion.Euler(0, currAngle, 0) * dir) * range;
                Gizmos.DrawLine(lastPoint, p);
                lastPoint = p;
            }

            Gizmos.DrawLine(lastPoint, p2);
        }

        public static void DrawLines(List<Vector3> points)
        {
            if (points.Count < 2) return;

            for (int i = 0; i < points.Count - 1; i++) Gizmos.DrawLine(points[i], points[i + 1]);
            Gizmos.DrawLine(points[^1], points[0]);
        }

        public static void DrawLines(List<Vector2> points, float y = 0)
        {
            DrawLines(points.Select(p => p.ToVector3(y)).ToList());
        }

        public static void DrawRectangleTwoPoints(Vector3 p1, Vector3 p2)
        {
            DrawLines(new List<Vector3>() {
                p1,
                new(p1.x, p1.y, p2.z),
                p2,
                new(p2.x, p2.y, p1.z)
            });
        }

        public static void DrawRectangleCenterExtent(Vector3 center, Vector3 extent)
        {
            DrawLines(new List<Vector3>() {
                center + extent / 2,
                center + new Vector3(extent.x, extent.y, -extent.z) / 2,
                center - extent / 2,
                center + new Vector3(-extent.x, extent.y, extent.z) / 2,
            });
        }

        public static void DrawSquare(Vector3 center, Vector2 size, Vector3 axis = default, float angle = 0.0f)
        {
            if (axis.magnitude == 0) axis = Vector3.up;

            Quaternion quat = Quaternion.LookRotation(axis) * Quaternion.AngleAxis(angle, axis);

            DrawLines(new List<Vector3>() {
                quat * new Vector3(size.x / 2, size.y / 2, 0.0f) + center,
                quat * new Vector3(size.x / 2, -size.y / 2, 0.0f) + center,
                quat * new Vector3(-size.x / 2, -size.y / 2, 0.0f) + center,
                quat * new Vector3(-size.x / 2, size.y / 2, 0.0f) + center,
            });
        }

        public static void DrawTriangle(Vector3 center, float radius, Vector3 axis = default)
        {
            if (axis.magnitude == 0) axis = Vector3.up;

            Quaternion quat = Quaternion.LookRotation(axis);

            float squareThree = Mathf.Sqrt(3);
            DrawLines(new List<Vector3>() {
                quat * new Vector3(0, 0, radius / 2) + center,
                quat * new Vector3(squareThree * radius / 4, 0, -radius / 4) + center,
                quat * new Vector3(-squareThree * radius / 4, 0, -radius / 4) + center
            });
        }
    }
}
