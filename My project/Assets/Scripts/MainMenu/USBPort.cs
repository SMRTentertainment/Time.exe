using UnityEngine;

public class USBPort : MonoBehaviour
{
    [SerializeField] private string actionName;
    
    private MenuManager manager;
    void Start()
    {
        manager = FindFirstObjectByType<MenuManager>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pendrive"))
        {
            manager.RegisterUSBConnection(actionName, transform.position);
        }
    }
}
