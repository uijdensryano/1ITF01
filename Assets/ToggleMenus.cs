using UnityEngine;

public class ToggleMenus : MonoBehaviour
{
    public GameObject EscapeMenu;
    public GameObject SettingsMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SettingsMenu.activeSelf == false){
            ToggleMenu();
            // Debug.Log("Entered");
        }
    }

    public void ToggleMenu(){
        EscapeMenu.SetActive(!EscapeMenu.activeSelf);
    }
}
