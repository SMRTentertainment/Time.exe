using UnityEngine;

public abstract class TrapBehaviour : MonoBehaviour
{
    protected bool active;

    public virtual void SetActive(bool value)
    {
        active = value;
    }
}
