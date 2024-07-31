using UnityEngine;
using UnityEngine.ProBuilder;

public class AdjustColliderToBezier : MonoBehaviour
{
    public ProBuilderMesh proBuilderMesh; // Assign your ProBuilder Mesh with the Bezier shape
    public SphereCollider sphereCollider; // Assign the Sphere Collider

    void Update()
    {
        if (proBuilderMesh != null && sphereCollider != null)
        {
            sphereCollider.radius = CalculateRequiredRadius();
        }
    }

    float CalculateRequiredRadius()
    {
        MeshRenderer meshRenderer = proBuilderMesh.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // Using the bounds from MeshRenderer, which should encompass the whole mesh
            return meshRenderer.bounds.extents.magnitude; // This gives the radius as the largest extent from the center to the boundary of the mesh
        }
        return 0f;
    }
}

