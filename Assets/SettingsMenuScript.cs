using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    /*
    Audiosource still needs to be assigned
    */

    /* public Slider volumeSlider;
    public AudioSource audioSource; */

    public Slider gammaSlider;
    public UnityEngine.Rendering.Universal.Light2D globalLight;

    void Start()
    {
        /* volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(setVolume); */

        gammaSlider.onValueChanged.AddListener(AdjustBrightness);
        gammaSlider.value = globalLight.intensity;
    }

    public void setVolume(float volume){
        /* audioSource.volume = volume; */
    }

    public void AdjustBrightness(float value)
    {
        globalLight.intensity = value;
    }
}
