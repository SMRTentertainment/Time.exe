using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;

    private IInteractable currentInteractable;

    private void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        currentInteractable?.Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable) &&
            interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}