using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class StartScreenManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Image introImage;
    public TextMeshProUGUI gameTitle;
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    [Header("Timing")]
    public float imageDelayBeforeFade = 1.5f;
    public float imageFadeDuration = 2f;
    public float textFadeInDuration = 2f;
    public float textStayDuration = 2f;
    public float textFadeOutDuration = 1.5f;
    public float sceneLoadDelay = 0.5f;  // Vertraging na klikken op Play

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip clickSound;

    [Header("Cursor")]
    public Texture2D customCursor;

    void Start()
    {
        // Zet aangepaste cursor met het midden van de afbeelding als klikpunt
        if (customCursor != null)
        {
            Vector2 centerHotspot = new Vector2(customCursor.width / 2f, customCursor.height / 2f);
            Cursor.SetCursor(customCursor, centerHotspot, CursorMode.Auto);
        }

        // Speel achtergrondmuziek
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        // Initialiseer UI-elementen
        gameTitle.canvasRenderer.SetAlpha(0f);
        introImage.canvasRenderer.SetAlpha(1f);
        playButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        StartCoroutine(AnimateIntro());
    }

    IEnumerator AnimateIntro()
    {
        yield return new WaitForSeconds(imageDelayBeforeFade);

        introImage.CrossFadeAlpha(0f, imageFadeDuration, false);
        gameTitle.CrossFadeAlpha(1f, textFadeInDuration, false);

        yield return new WaitForSeconds(Mathf.Max(imageFadeDuration, textFadeInDuration) + textStayDuration);

        gameTitle.CrossFadeAlpha(0f, textFadeOutDuration, false);
        yield return new WaitForSeconds(textFadeOutDuration);

        playButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    void PlayClickSound()
    {
        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }

    public void OnPlayButton()
    {
        PlayClickSound();
        StartCoroutine(LoadSceneWithDelay("Tutorial")); // ← Pas deze naam aan naar jouw scène!
    }

    IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(sceneName);
    }

    public void OnOptionsButton()
    {
        PlayClickSound();
        Debug.Log("Options menu openen...");
    }

    public void OnQuitButton()
    {
        PlayClickSound();
        Debug.Log("Game wordt afgesloten.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
