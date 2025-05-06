using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    /*
    Known bugs:
    Menu toggalable in settings
    */
    
    public GameObject CurrentMenu;

    public void QuitGame(){
        Application.Quit();
        // Debug.Log("FlipperFlopper");
    }
}
