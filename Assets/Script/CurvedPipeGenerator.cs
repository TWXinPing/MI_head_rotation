using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CurvedPipeGenerator : MonoBehaviour
{
    public float curveRadius = 5f;       // Radius of the curve
    public float curveAngle = 90f;       // Total angle of the curve in degrees
    public int segments = 20;            // Number of segments in the cylinder

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        Vector3[] vertices = mesh.vertices;
        float angleStep = curveAngle / segments;
        float currentAngle = 0f;

        for (int i = 0; i < segments + 1; i++)
        {
            float radianAngle = currentAngle * Mathf.Deg2Rad;
            float x = Mathf.Sin(radianAngle) * curveRadius;
            float z = Mathf.Cos(radianAngle) * curveRadius;

            for (int j = 0; j < vertices.Length; j++)
            {
                vertices[j] = RotatePoint(vertices[j], Vector3.up, -currentAngle * Mathf.Deg2Rad);
                vertices[j] += new Vector3(x, 0, z);
            }

            currentAngle += angleStep;
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    Vector3 RotatePoint(Vector3 point, Vector3 axis, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, axis);
        return rotation * point;
    }
}