using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInput : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseAction;

    private void OnEnable()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPause;
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
        pauseAction.action.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        GameStateManager.Instance.TogglePause();
    }
}
