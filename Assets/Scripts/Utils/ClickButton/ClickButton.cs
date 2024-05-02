using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    void FixedUpdate()
    {
        // Iterate over all touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            // Check if the touch phase is began
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                // Get the touch position and cast a ray from it
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;

                // Check if the ray hits any collider
                if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //layerMask
                {
                    // Get the game object that was hit
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject.name == name)
                    {
                        Action();
                    }
                }
            }
        }
    }

    public virtual void Action() { }
}
