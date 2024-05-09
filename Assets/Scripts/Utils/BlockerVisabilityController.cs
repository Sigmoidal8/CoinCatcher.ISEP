using UnityEngine;

public class BlockerVisibilityController : MonoBehaviour
{
    public Transform Player;
    public Collider[] BlockerColliders;
    public float VisibleDistance = 10f;
    public AudioSource AudioSource;
    private bool SoundPlayed = false;

    private void Update()
    {
        bool anyColliderVisible = false;
        foreach (Collider collider in BlockerColliders)
        {
            Vector3 closestPoint = collider.ClosestPointOnBounds(Player.position);
            float distanceToPlayer = Vector3.Distance(closestPoint, Player.position);

            if (distanceToPlayer <= VisibleDistance)
            {
                Debug.Log(distanceToPlayer);

                float alpha = Mathf.Clamp01(1f - (distanceToPlayer / VisibleDistance));

                Material material = collider.GetComponent<Renderer>().material;
                Color color = material.color;
                color.a = alpha;
                material.color = color;

                // Make the renderer associated with the blocker invisible
                Renderer renderer = collider.GetComponent<Renderer>();
                renderer.enabled = true;
                anyColliderVisible = true;
                if (distanceToPlayer <= 0.5)
                {
                    if (!AudioSource.isPlaying && !SoundPlayed)
                    {
                        SoundPlayed = true;
                        AudioSource.Play();
                    }
                }
            }
            else
            {
                // Make the renderer associated with the blocker invisible
                Renderer renderer = collider.GetComponent<Renderer>();
                renderer.enabled = false;
            }
        }
        if (!anyColliderVisible){
            SoundPlayed = false;
        }
    }
}
