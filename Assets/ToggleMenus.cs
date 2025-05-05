using UnityEngine;

public class ToggleMenus : MonoBehaviour
{
    public GameObject EscapeMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            ToggleMenu();
            // Debug.Log("Entered");
        }
    }

    public void ToggleMenu(){
        EscapeMenu.SetActive(!EscapeMenu.activeSelf);
    }
}
