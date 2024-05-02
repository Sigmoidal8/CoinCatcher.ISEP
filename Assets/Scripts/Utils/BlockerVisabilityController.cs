using UnityEngine;

public class BlockerVisibilityController : MonoBehaviour
{
    public Transform Player;
    public Collider[] BlockerColliders;
    public float VisibleDistance = 10f;

    private void Update()
    {
        foreach (Collider collider in BlockerColliders)
        {
            Bounds colliderBounds = collider.bounds;
            Vector3 closestPoint = colliderBounds.ClosestPoint(Player.position);
            float distanceToPlayer = Vector3.Distance(closestPoint, Player.position);

            if (distanceToPlayer <= VisibleDistance)
            {
                float alpha = Mathf.Clamp01(1f - (distanceToPlayer / VisibleDistance));

                Material material = collider.GetComponent<Renderer>().material;
                Color color = material.color;
                color.a = alpha;
                material.color = color;

                // Make the renderer associated with the blocker invisible
                Renderer renderer = collider.GetComponent<Renderer>();
                renderer.enabled = true;
            }
            else
            {
                // Make the renderer associated with the blocker invisible
                Renderer renderer = collider.GetComponent<Renderer>();
                renderer.enabled = false;
            }
        }
    }
}
