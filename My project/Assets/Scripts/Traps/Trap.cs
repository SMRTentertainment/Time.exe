using UnityEngine;

public abstract class TrapBase : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        ActivateTrap();
    }

    protected abstract void ActivateTrap();
}
