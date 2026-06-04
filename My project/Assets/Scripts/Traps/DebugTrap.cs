using UnityEngine;

public class DebugTrap : TrapBase
{
    protected override void ActivateTrap()
    {
        Debug.Log($"La trampa fue activada. gil");
    }
}
