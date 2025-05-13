using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endscreenManager : MonoBehaviour
{
    public GameObject endscreenFailure;
    public GameObject Line;
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
        Line.SetActive(false);
        endscreenFailure.SetActive(true);
    }

    public void CloseEndScreenFailure()
    {
        endscreenFailure.SetActive(false);
    }

    public void OpenEndScreenSuccess(int collectedStars)
    {
        Line.SetActive(false);
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

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Home(){
        SceneManager.LoadScene("Startscreen");
    }
}
