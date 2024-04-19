using UnityEngine;

public class Transparency : MonoBehaviour
{
    public Color hitColor = Color.blue; // Desired color on collision
    private Color originalColor; // To store the original color

    void Start()
    {   
        SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();
        if (renderer != null)
        {
            // Store the original color at the start
            originalColor = renderer.material.GetColor("_Color");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Box"
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Enemy"))
        {
            SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();
            if (renderer != null)
            {
                // Directly change the color of the MeshRenderer's material
                renderer.material.SetColor("_Color", hitColor);
                Invoke("ResetColor", 3f);
            }
        }
    }

    // void OnCollisionExit(Collision collision)
    // {
    //     // Check if the exited object had the tag "Box"
    //     if (collision.gameObject.CompareTag("Box"))
    //     {
    //         // Invoke the ResetColor method after 2 seconds
    //         Invoke("ResetColor", 2f);
    //     }
    // }

        void ResetColor()
    {
        SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();
        // Reset the color of the renderer's material to the original color
        if (renderer != null)
        {
            renderer.material.SetColor("_Color", originalColor);
        }
    }
}
