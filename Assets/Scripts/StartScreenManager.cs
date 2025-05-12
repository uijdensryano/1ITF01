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
    public float imageDelayBeforeFade = 1.5f;          // Nieuw: hoe lang de afbeelding volledig zichtbaar blijft
    public float imageFadeDuration = 2f;
    public float textFadeInDuration = 2f;
    public float textStayDuration = 2f;
    public float textFadeOutDuration = 1.5f;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip clickSound;

    [Header("Cursor")]
    public Texture2D customCursor;
    public Vector2 cursorHotspot = Vector2.zero;

    void Start()
    {
        // Zet aangepaste cursor
        if (customCursor != null)
        {
            Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
        }

        // Speel achtergrondmuziek
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        // Initialiseer UI-elementen
        gameTitle.canvasRenderer.SetAlpha(0f); // Titeltekst onzichtbaar
        introImage.canvasRenderer.SetAlpha(1f); // Afbeelding volledig zichtbaar
        playButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        StartCoroutine(AnimateIntro());
    }

    IEnumerator AnimateIntro()
    {
        // Laat afbeelding eerst zichtbaar staan
        yield return new WaitForSeconds(imageDelayBeforeFade);

        // Start tegelijk: afbeelding vervaagt weg, tekst verschijnt
        introImage.CrossFadeAlpha(0f, imageFadeDuration, false);
        gameTitle.CrossFadeAlpha(1f, textFadeInDuration, false);

        // Wacht tot de tekst volledig is verschenen en even blijft
        yield return new WaitForSeconds(Mathf.Max(imageFadeDuration, textFadeInDuration) + textStayDuration);

        // Vervaag de tekst
        gameTitle.CrossFadeAlpha(0f, textFadeOutDuration, false);
        yield return new WaitForSeconds(textFadeOutDuration);

        // Toon de knoppen
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
        SceneManager.LoadScene("GameScene");
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
