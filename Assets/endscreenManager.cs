using UnityEngine;
using UnityEngine.UI;

public class endscreenManager : MonoBehaviour
{
    public GameObject endscreenFailure;
    public Button restartButton;

    private int starsCollected = 2;

    public GameObject endscreenSuccess;
    public Button nextButton;

    public Image[] stars; // ✅ changed from SpriteRenderer[] to Image[]
    
    void Start()
    {
        endscreenFailure.SetActive(false);
        endscreenSuccess.SetActive(false);
        ResetStars();
    }

    public void OpenEndScreenFailure()
    {
        endscreenFailure.SetActive(true);
    }

    public void CloseEndScreenFailure()
    {
        endscreenFailure.SetActive(false);
    }

    public void OpenEndScreenSuccess(int collectedStars)
    {
        Debug.Log("help");
        starsCollected = Mathf.Clamp(collectedStars, 0, stars.Length);
        UpdateStarsDisplay();
        endscreenSuccess.SetActive(true);
    }

    public void CloseEndScreenSuccess()
    {
        endscreenSuccess.SetActive(false);
    }

    private void UpdateStarsDisplay()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            Color starColor = stars[i].color;
            bool isActive = i < starsCollected;
            starColor.a = isActive ? 1f : 0.3f;
            stars[i].color = starColor;

            Animator animator = stars[i].GetComponent<Animator>();
            if (animator != null)
            {
                if (isActive)
                {
                    animator.SetTrigger("Activate"); // Make sure you have this trigger in your Animator
                }
                else
                {
                    animator.ResetTrigger("Activate");
                    animator.Play("Idle", -1, 0f); // Reset to idle or dim state if needed
                }
            }
        }
    }

    private void ResetStars()
    {
        foreach (var star in stars)
        {
            Color starColor = star.color;
            starColor.a = 0.3f; // Dim the star
            star.color = starColor;
        }
    }
}
