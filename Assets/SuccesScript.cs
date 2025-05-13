using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccesScript : MonoBehaviour
{
    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
