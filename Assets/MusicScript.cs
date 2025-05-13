using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public GameObject music;

    void Awake()
    {
        DontDestroyOnLoad(music);
    }
}
