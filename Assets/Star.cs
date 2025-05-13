using UnityEngine;

public class Star : MonoBehaviour
{
    // This will reference a script that tracks the score
    public ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            // Increase the score
            scoreManager.AddPoint();

            // Make the star disappear
            gameObject.SetActive(false); // or use Destroy(gameObject);
        }
    }
}