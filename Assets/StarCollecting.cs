using UnityEngine;
using UnityEngine.UI;

public class StarCollector : MonoBehaviour
{
    public Text starCounterText; // Assign this in the Inspector
    private int starCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            Debug.Log("Star collected!");
            Destroy(other.gameObject);
            starCount++;
            UpdateStarCounterUI();
        }
    }

    void UpdateStarCounterUI()
    {
        if (starCounterText != null)
        {
            starCounterText.text = "Stars: " + starCount;
        }
    }
}
