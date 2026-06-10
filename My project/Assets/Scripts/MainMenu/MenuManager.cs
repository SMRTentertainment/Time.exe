using UnityEngine;
using Unity.Cinemachine;

public class MenuManager : MonoBehaviour
{
    private bool isConected = false;

    [Header("Mouse configuration")] [SerializeField]
    private Transform pendriveCursor;
    [SerializeField] private float zDistance = 10f;


    [Header("Sound configuration")]
    [SerializeField] private AudioSource USBsfx;
    [SerializeField] private AudioClip USBConected;
    [SerializeField] private AudioClip USBDisconected;

    [Header("Our New Managers")] 
    [SerializeField] private ScreenTransitionManager transitionManager;
    [SerializeField] private MenuUI uiManager;

void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
        if (USBsfx == null) {USBsfx = GetComponent<AudioSource>();}
        
        if (transitionManager != null) transitionManager.ResetCameras();
        if (uiManager != null) uiManager.HideAllMenus();
    }

    void Update()
    {
        if (isConected)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DisconnectUSB();
            }
            return;
        }
        MovePendriveWithMouse();
    }

    public void RegisterUSBConnection(string action, Vector3 PortPosition)
    {
        if (isConected) return; 
        isConected = true;

        if (pendriveCursor != null) pendriveCursor.position = PortPosition;
        PlaySound(USBConected);

        if (transitionManager != null)
        {
            transitionManager.StartTransitionSequence(() => 
            {
                if (uiManager != null)
                {
                    uiManager.ShowMenu(action);
                }
            });
        }
    }
    
    private void DisconnectUSB()
    {
        isConected = false;
        PlaySound(USBDisconected);
        
        if (transitionManager != null) transitionManager.ResetCameras();
        if (uiManager != null) uiManager.HideAllMenus();
    }
    private void MovePendriveWithMouse()
    {
        if (pendriveCursor != null)
        {
            if (pendriveCursor != null)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = zDistance;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
                pendriveCursor.position = worldPosition;
            }
        }
    }
    private void PlaySound(AudioClip clip)
    {
        if (USBsfx != null && clip != null)
        {
            USBsfx.PlayOneShot(clip);
        }
    }
}
