using System;

public static class RipCurrentEvents
{
    // Event: Fired when a rip current is activated in a specific water sector
    public static Action<int> OnRipCurrentActivated;
}
