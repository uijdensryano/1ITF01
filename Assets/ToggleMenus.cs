using UnityEngine;

public class ToggleMenus : MonoBehaviour
{
    public GameObject EscapeMenu;
    public GameObject SettingsMenu;
    public GameObject Background;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SettingsMenu.activeSelf == false){
            ToggleMenu();
            // Debug.Log("Entered");
        }
    }

    public void ToggleMenu(){
        Background.SetActive(!Background.activeSelf);
        EscapeMenu.SetActive(!EscapeMenu.activeSelf);
    }
}
