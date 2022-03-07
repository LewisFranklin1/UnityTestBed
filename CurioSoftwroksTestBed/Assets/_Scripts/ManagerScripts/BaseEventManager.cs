using System;
using UnityEngine;

public static class BaseEventManager
{
    public delegate void toggleLightSwitchDelegate(Collider collider);
    public static event toggleLightSwitchDelegate toggleLightSwitchEvent;

    public static void toggleLightSwitch(Collider collider)
    {
        toggleLightSwitchEvent?.Invoke(collider);
    }
}
