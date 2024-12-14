using UnityEngine;

public class UIParallax : MonoBehaviour
{
    public RectTransform[] layers; // Assign your background layers here
    public float[] parallaxSpeeds; // The parallax speed for each layer

    private Vector2 previousMousePosition;

    void Start()
    {
        // Initialize previous mouse position to current position
        previousMousePosition = Input.mousePosition;
    }

    void Update()
    {
        // Calculate the mouse movement delta
        Vector2 mouseDelta = (Vector2)Input.mousePosition - previousMousePosition;

        // Apply parallax effect to each layer
        for (int i = 0; i < layers.Length; i++)
        {
            if (i < parallaxSpeeds.Length)
            {
                float speed = parallaxSpeeds[i];
                layers[i].anchoredPosition += mouseDelta * speed * Time.deltaTime;
            }
        }

        // Update the previous mouse position
        previousMousePosition = Input.mousePosition;
    }
}
