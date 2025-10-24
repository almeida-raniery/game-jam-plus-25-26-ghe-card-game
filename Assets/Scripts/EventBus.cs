using System;
using UnityEngine;

public static class EventBus
{
    public static Action onGameOverRequestedEvent;

    public static void GameOverRequestedEvent() 
    {
        onGameOverRequestedEvent?.Invoke();
    }
}
