using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [Header("Canvas Panels")]
    [SerializeField] private GameObject canvasPlay;
    [SerializeField] private GameObject canvasOptions;
    [SerializeField] private GameObject canvasExit;
    
    public void ShowMenu(string action)
    {
        canvasPlay.SetActive(false);
        canvasOptions.SetActive(false);
        canvasExit.SetActive(false);

        if (action == "PLAY") canvasPlay.SetActive(true);
        if (action == "OPTIONS") canvasOptions.SetActive(true);
        if (action == "EXIT") canvasExit.SetActive(true);
        
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideAllMenus()
    {
        canvasPlay.SetActive(false);
        canvasOptions.SetActive(false);
        canvasExit.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
