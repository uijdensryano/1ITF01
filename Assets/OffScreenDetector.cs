using UnityEngine;
using UnityEngine.SceneManagement;

public class OffScreenDetector : MonoBehaviour
{
    public float bottomLimit = -10f; // Y-position below which the car is considered off screen
    public float horizontalLimit = 30f; // Optional: X-limit if needed
    public bool checkHorizontal = false;

    public endscreenManager endScreenManager;

    void Update()
    {
        Vector3 carPosition = transform.position;

        // Check vertical fall
        if (carPosition.y < bottomLimit || (checkHorizontal && Mathf.Abs(carPosition.x) > horizontalLimit))
        {
            Debug.Log("Car went off screen!");

            if (endScreenManager != null)
            {
                endScreenManager.OpenEndScreenFailure();
            }
            else
            {
                // Fallback: restart the current scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            // Disable further checking
            enabled = false;
        }
    }
}
