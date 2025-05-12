using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Reload(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToStart(){
        SceneManager.LoadScene("Startscreen");
    }
}
