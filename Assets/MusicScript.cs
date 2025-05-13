using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public GameObject music;

    public void Awake()
    {
        DontDestroyOnLoad(music);
    }
}
