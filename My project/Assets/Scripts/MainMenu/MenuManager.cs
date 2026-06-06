using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Mouse configuration")] 
    
    [SerializeField] private Transform pendriveCursor;
    [SerializeField] private float zDistance = 10f;
    
    [Header("Sound configuration")]
    [SerializeField] private AudioSource USBsfx; 
    [SerializeField] private AudioClip USBConected;   
    [SerializeField] private AudioClip USBDisconected; 
    private bool isConected = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        if (USBsfx == null)
        {
            USBsfx = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isConected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isConected = false;
                Debug.Log("Pendrive deconectado");
                PlaySound(USBDisconected);
            }
            return;
        }

        if (pendriveCursor is not null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = zDistance;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            pendriveCursor.position = worldPosition;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (USBsfx != null && clip != null)
        {
            USBsfx.PlayOneShot(clip);
        }
    }

    public void RegisterUSBConnection(string action, Vector3 PortPosition)
    {
        if (isConected) return;

        isConected = true;
        pendriveCursor.position = PortPosition;
        PlaySound(USBConected);

        switch (action)
        {
            case "PLAY":
                PlayButton();
                break;
            case "OPTIONS":
                OptionsButton();
                break;
            case "EXIT":
                ExitButton();
                break;
        }
    }

    private void PlayButton()
    {
        Debug.Log("Conectado en PLAY: Moviendo cámara al monitor...");
    }

    private void OptionsButton()
    {
        Debug.Log("Conectado en OPTIONS: Abriendo menú de opciones...");
    }

    private void ExitButton()
    {
        Debug.Log("Conectado en EXIT: Cerrando aplicación...");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}